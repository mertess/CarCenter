﻿using System;
using System.Collections.Generic;
using System.Text;
using DocumentFormat.OpenXml.Wordprocessing;

namespace CarCenterBusinessLogic.HelperModels
{
    public class WordParagraphProperties
    {
        public string Size { get; set; }
        public bool Bold { get; set; }
        public JustificationValues JustificationValues { get; set; }
    }
}
