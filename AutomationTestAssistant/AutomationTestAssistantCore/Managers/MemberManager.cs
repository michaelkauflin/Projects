﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATADataModel;
using System.Security.Cryptography;
using System.IO;
using System.Net.Mail;

namespace AutomationTestAssistantCore
{
    public class MemberManager
    {
        public const string Salt = "5B-4B-05-AD-A5-1D-3E-C5-E5-62-8D-32-74-B4-8A-0A-49-74-31-25-E4-8C-77-34-21-66-FE-64-E8-14-8C-74";

        public Member RetrieveMemberByCredentials(ATAEntities context, string userName, string password)
        {
            string currentUserHashedPassword = CreatePasswordHash(password);
            Member currentMember = context.Members.Where(m => m.UserName.Equals(userName) && m.Password.Equals(currentUserHashedPassword)).FirstOrDefault();
         
            return currentMember;
        }

        public Statuses GetMemberStatus(Member member)
        {
            Statuses status = (Statuses)member.StatusId;

            return status;
        }

        public Member GetMemberByUserName(ATAEntities context, string userName)
        {
            Member member = context.Members.Where(x => x.UserName.Equals(userName)).FirstOrDefault();

            return member;
        }

        public Member CreateUser(ATAEntities context, string userName, string password, string email, string tfsUserName, string role, List<string> teams, string comment)
        {
            string hashedPassword = CreatePasswordHash(password);
            MemberRole currentMemberRole = ATACore.Managers.MemberRoleManager.GetMemberRoleByRoleName(context, role);
            Member newMember = new Member()
            {
                UserName = userName,
                Password = hashedPassword,
                Email = email,
                Comment = comment,
                TfsUserName = tfsUserName,
                MemberRoleId = currentMemberRole.MemberRoleId
            };
            newMember.StatusId = (int)Statuses.ToBeApproved;
            AddTeamsToNewMember(context, teams, newMember);
            context.Members.Add(newMember);
            context.SaveChanges();
            
            return newMember;
        }

        private static void AddTeamsToNewMember(ATAEntities context, List<string> teams, Member newMember)
        {
            teams.ForEach(t =>
            {
                Team currentTeam = ATACore.Managers.TeamManager.GetTeamByName(context, t);
                newMember.Teams.Add(currentTeam);
            });
        }

        public void ApproveUser(ATAEntities context, string userName, MemberRoles newMemberRole)
        {
            Member memberToBeApproved = context.Members.Where(x => x.UserName.Equals(userName)).FirstOrDefault();
            ActivationCode ac = GenerateNewActivationCode();
            memberToBeApproved.ActivationCodes.Add(ac);
            memberToBeApproved.UserMemberRole = newMemberRole;
            memberToBeApproved.StatusId = (int)Statuses.PendingActivation;
            SendActivationEmail(memberToBeApproved);
            context.SaveChanges();
        }

        public void ActivateUser(ATAEntities context, string userName)
        {
            Member memberToBeApproved = GetMemberByUserName(context, userName);
            memberToBeApproved.StatusId = (int)Statuses.Active;
            context.SaveChanges();
        }

        private void SendActivationEmail(Member memberToBeApproved)
        {
            var message = new MailMessage();
            message.To.Add(memberToBeApproved.Email);
            var client = new SmtpClient();
            message.Body = memberToBeApproved.ActivationCodes.Last(x => x.Code != null).Code;
            client.Send(message);
        }

        private ActivationCode GenerateNewActivationCode()
        {
            return new ActivationCode()
            {
                Code = Guid.NewGuid().ToString(),
                CreationDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddDays(3)
            };
        }

        private byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        private string CreatePasswordHash(string password)
        {
            byte[] saltBytes = GetBytes(Salt);
            PasswordDeriveBytes passProvider = new PasswordDeriveBytes(password, saltBytes);
            string hashedPassword = BitConverter.ToString(passProvider.GetBytes(32));
            return hashedPassword;
        }

        public List<Member> GetAllMembersForApproval(ATAEntities context)
        {
            var query = context.Members.Where(m => m.StatusId.Equals(2));
            return query.ToList();
        }

        public string GetTfsUserNameByUserName(ATAEntities context, string currentUserName)
        {
            Member m = GetMemberByUserName(context, currentUserName);
            return m.TfsUserName;
        }
    }
}
