﻿using System;
using System.Linq;

namespace YouTube.SDK.Entities.Contracts
{
    /// <summary>
    /// Defines Base YouTube Song Properties
    /// </summary>
    public interface IYouTubeSong
    {
        /// <summary>
        /// Gets or sets the artist.
        /// </summary>
        /// <value>
        /// The artist.
        /// </value>
        string Artist { get; set; }

        /// <summary>
        /// Gets or sets the song identifier.
        /// </summary>
        /// <value>
        /// The song identifier.
        /// </value>
        string SongId { get; set; }

        /// <summary>
        /// Gets or sets you tube song title.
        /// </summary>
        /// <value>
        /// You tube song title.
        /// </value>
        string Title { get; set; }

        /// <summary>
        /// Gets or sets the play list item identifier.
        /// </summary>
        /// <value>
        /// The play list item identifier.
        /// </value>
        string PlayListItemId { get; set; }

        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        /// <value>
        /// The duration.
        /// </value>
        ulong? Duration { get; set; }

        /// <summary>
        /// Gets or sets the original title.
        /// </summary>
        /// <value>
        /// The original title.
        /// </value>
        string OriginalTitle { get; set; }

        /// <summary>
        /// Gets or sets the song unique identifier.
        /// </summary>
        /// <value>
        /// The song unique identifier.
        /// </value>
        Guid SongGuid { get; set; }
    }
}
