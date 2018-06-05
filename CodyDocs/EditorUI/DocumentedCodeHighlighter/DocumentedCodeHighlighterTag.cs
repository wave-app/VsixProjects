﻿using CodyDocs.Models;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;

namespace CodyDocs.EditorUI.DocumentedCodeHighlighter
{
    public class DocumentedCodeHighlighterTag : TextMarkerTag
    {
        public string DocumentationFragmentText { get; private set; }
        public ITrackingSpan TrackingSpan { get; set; }
        public ITextBuffer TextBuffer { get; set; }

        public DocumentedCodeHighlighterTag(string fragment, ITrackingSpan trackingSpan, ITextBuffer buffer) : base("MarkerFormatDefinition/DocumentedCodeFormatDefinition")
        {
            DocumentationFragmentText = fragment;
            TrackingSpan = trackingSpan;
            TextBuffer = buffer;
        }

    }
}
