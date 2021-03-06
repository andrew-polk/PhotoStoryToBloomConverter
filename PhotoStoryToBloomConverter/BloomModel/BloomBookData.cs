﻿using System.Collections.Generic;
using System.Linq;
using PhotoStoryToBloomConverter.BloomModel.BloomHtmlModel;

namespace PhotoStoryToBloomConverter.BloomModel
{
    //Comparable to a collection of the bloom 'data' divs
    public class BloomBookData
    {
        public string LanguagesOfBook;
        public string StyleNumberSequence;
        public string LicenseUrl;
        public string LicenseImage;
        public string Title;
        public string[] ContentLanguages;

        //All localized variables are expected to have similar indexes 
        //to their languages location in ContentLanguages
        public string[] LocalizedBookTitle;
        public string[] LocalizedSmallCoverCredits;
        public string[] LocalizedOriginalContributions;
        public string[] LocalizedFunding;
        public string[] LocalizedOriginalAcknowledgments;
        public string[] LocalizedInsideFrontCover;
        public string[] LocalizedInsideBackCover;
        public string[] LocalizedOutsideBackCover;
        public string[] LocalizedLicenseDescription;

        public List<List<Dictionary<string, string>>> LocalizedNarrationList;

        public static BloomBookData DefaultBloomBookData(string title)
        {
            return new BloomBookData
            {
                Title = title,
                LanguagesOfBook = "English",
                StyleNumberSequence = "0",
                LicenseUrl = "http://creativecommons.org/licenses/by/4.0",
                LicenseImage = "license.png",
                ContentLanguages = new [] { "en" },
                LocalizedBookTitle = new [] { "" },
                LocalizedSmallCoverCredits = new[] { "" },
                LocalizedOriginalContributions = new[] { "" },
                LocalizedFunding = new[] { "" },
                LocalizedOriginalAcknowledgments = new[] { "" },
                LocalizedInsideFrontCover = new[] { "" },
                LocalizedInsideBackCover = new[] { "" },
                LocalizedOutsideBackCover = new[] { "" },
                LocalizedLicenseDescription = new[] { "You are free to make commercial use of this work. You may adapt and add to this work. You must keep the copyright and credits for authors, illustrators, etc." },
            };
        }

        public Div ConvertToHtml()
        {
            var dataDiv = new Div
            {
                Id = "bloomDataDiv",
                Divs = new List<Div>
                {
                    new Div { DataBook = "styleNumberSequence", Lang = "*", SimpleText = StyleNumberSequence},
                    new Div { DataBook = "languagesOfBook", Lang = "*", SimpleText = LanguagesOfBook},
                    //new Div { DataBook = "coverImage", Lang = "*", SimpleText = CoverImage},
                    new Div { DataBook = "licenseUrl", Lang = "*", SimpleText = LicenseUrl},
                }
            };
            dataDiv.Divs.AddRange(ContentLanguages.Select((lang, index) => new Div { DataBook = string.Format("contentLanguage{0}", index+1), Lang = "*", SimpleText = lang }).ToArray());
            dataDiv.Divs.AddRange(LocalizedBookTitle.Select((lang, index) => new Div { DataBook = "bookTitle", Lang = ContentLanguages[index], FormattedText = new Paragraph { Text = Title } }).ToArray());
            dataDiv.Divs.AddRange(LocalizedSmallCoverCredits.Select((credits, index) => new Div { DataBook = "smallCoverCredits", Lang = ContentLanguages[index], FormattedText = new Paragraph { Text = credits } }).ToArray());
            dataDiv.Divs.AddRange(LocalizedOriginalContributions.Select((contributions, index) => new Div { DataBook = "originalContributions", Lang = ContentLanguages[index], FormattedText = new Paragraph { Text = contributions } }).ToArray());
            dataDiv.Divs.AddRange(LocalizedOriginalAcknowledgments.Select((acknowledgments, index) => new Div { DataBook = "originalAcknowledgments", Lang = ContentLanguages[index], FormattedText = new Paragraph { Text = acknowledgments } }).ToArray());
            dataDiv.Divs.AddRange(LocalizedFunding.Select((funding, index) => new Div { DataBook = "funding", Lang = ContentLanguages[index], FormattedText = new Paragraph { Text = funding } }).ToArray());
            dataDiv.Divs.AddRange(LocalizedInsideFrontCover.Select((insideFrontCover, index) => new Div { DataBook = "insideFrontCover", Lang = ContentLanguages[index], FormattedText = new Paragraph { Text = insideFrontCover } }).ToArray());
            dataDiv.Divs.AddRange(LocalizedInsideBackCover.Select((insideBackCover, index) => new Div { DataBook = "insideBackCover", Lang = ContentLanguages[index], FormattedText = new Paragraph { Text = insideBackCover } }).ToArray());
            dataDiv.Divs.AddRange(LocalizedOutsideBackCover.Select((outsideBackCover, index) => new Div { DataBook = "outsideBackCover", Lang = ContentLanguages[index], FormattedText = new Paragraph { Text = outsideBackCover } }).ToArray());
            dataDiv.Divs.AddRange(LocalizedLicenseDescription.Select((licenseDescription, index) => new Div { DataBook = "licenseDescription", Lang = ContentLanguages[index], FormattedText = new Paragraph { Text = licenseDescription } }).ToArray());
            return dataDiv;
        }
    }
}