﻿// <copyright file="PrompDialogRichTextBoxWindow.xaml.cs" company="CodePlex">
// https://testcasemanager.codeplex.com/ All rights reserved.
// </copyright>
// <author>Anton Angelov</author>
namespace TreeNotebook.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Shapes;
    using AntaresFramework.Core.Managers;
    using FirstFloor.ModernUI.Windows.Controls;

    /// <summary>
    /// Initializes promo dialog window
    /// </summary>
    public partial class PrompDialogRichTextBoxWindow : ModernWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PrompDialogRichTextBoxWindow"/> class.
        /// </summary>
        public PrompDialogRichTextBoxWindow()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Handles the Closing event of the ModernWindow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void ModernWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (RegistryManager.ReadIsWindowClosedFromX())
            {
                RegistryManager.WriteIsCanceledTitlePromtDialog(true);
            }
        }
    }
}
