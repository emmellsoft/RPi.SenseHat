////////////////////////////////////////////////////////////////////////////
//
//  This file is part of RPi.SenseHat.Demo
//
//  Copyright (c) 2019, Mattias Larsson
//
//  Permission is hereby granted, free of charge, to any person obtaining a copy of
//  this software and associated documentation files (the "Software"), to deal in
//  the Software without restriction, including without limitation the rights to use,
//  copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the
//  Software, and to permit persons to whom the Software is furnished to do so,
//  subject to the following conditions:
//
//  The above copyright notice and this permission notice shall be included in all
//  copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
//  INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A
//  PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
//  HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
//  OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
//  SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using Emmellsoft.IoT.RPi.SenseHat;
using Emmellsoft.IoT.RPi.SenseHat.Fonts;
using Emmellsoft.IoT.RPi.SenseHat.Fonts.MultiColor;
using System;
using System.Collections.Generic;

namespace Emmellsoft.IoT.RPi.SenseHat.Demo.Common.Demos
{
    /// <summary>
    /// Multi-color scroll-text.
    /// </summary>
    public class MultiColorScrollText : SenseHatDemo
    {
        private readonly string _scrollText;

        public MultiColorScrollText(ISenseHat senseHat, string scrollText)
            : base(senseHat)
        {
            _scrollText = scrollText;
        }

        public override void Run()
        {
#if WINDOWS_UWP
            Image image = ImageSupport.GetImage(new Uri("ms-appx:///Assets/ColorFont.png"));
#else

            #region Serialized Image

            // The image below is serialized using the ImageSerializer.Serialize() method.
            // Please see the "SerializeImages" method in the RPi.SenseHat.Tools project.
            const string serializedImage =
        "AOICAAAJAAAAAAAA////////////////////AAAA////////////////////////////AAAA////////////////////////////AAAA////////////////////////////AAAA////////////////////////////AAAA////////////////////////////AAAA" +
        "////////////////////////////AAAA////////////////////////////AAAA////////////////////////////AAAA////////////////////AAAA////////////////////////////AAAA////////////////////////////AAAA////////////////" +
        "////////////AAAA////////////////////////////////AAAA////////////////////////////AAAA////////////////////////////AAAA////////////////////////////AAAA////////////////////////////AAAA////////////////////" +
        "////////AAAA////////////////////////////AAAA////////////////////////////AAAA////////////////////////////AAAA////////////////////////////AAAA////////////////////////////////AAAA////////////////////////" +
        "////AAAA////////////////////////////AAAA////////////////////////////AAAA////////////////////////////AAAA////////////////////////////AAAA////////////////////////////AAAA////////////////////////////AAAA" +
        "////////////////////////////AAAA////////////////////////AAAA////////////////////////////AAAA////////////////////////AAAA////////////////////////////AAAA////////////////////////////AAAA////////////////" +
        "////////AAAA////////////////////////////AAAA////////////////////////////AAAA////////////////////AAAA////////////////////AAAA////////////////////////////AAAA////////////////////AAAA////////////////////" +
        "////////////AAAA////////////////////////////AAAA////////////////////////////AAAA////////////////////////////AAAA////////////////////////////AAAA////////////////////////////AAAA////////////////////////" +
        "////AAAA////////////////////////////AAAA////////////////////////////AAAA////////////////////////////AAAA////////////////////////////////AAAA////////////////////////////AAAA////////////////////////////" +
        "AAAA////////////////////////////AAAA////////////////////////AAAA////////////////////////AAAA////////////////////////////AAAA////////////////////////////AAAA////////////////////////////AAAA////////////" +
        "////////////////AAAA////////////////////////////AAAA////////////////////////////AAAA////////////////////////////AAAA////////////////////////////AAAA////////////////////////////AAAA////////////////////" +
        "////////AAAA////////////////////////////AAAA////////////////////////////AAAA////////////////////////////AAAA////////////AAAA////////////////AAAA////////////////////////////AAAA////////////AAAA////////" +
        "////////////////////AAAA////////////////////////////////////AAAA////////////////////////////AAAA////////////////////////////AAAA////////////////////////////////AAAA////////////////////////////AAAA////" +
        "////////////////////////AAAA////////////////////////////////////AAAA////////////AAAA////////////////AAAA////////////////////////////////AAAA////////////////////////////////AAAA////////////////////////" +
        "////AAAA////////////////////////////AAAA////////////////////AAAA////////////////////AAAA////////////////////AAAA////////////////////AAAA/////////////////////////////wD//wD//wD//wD//wD//wD//wD//wD//wD/" +
        "CgCyCgCy/wD//wD//wD//wD/CgCyCgCyCgCyCgCyCgCy/wD//wD//wD//wD/CgCyCgCyCgCyCgCy/wD//wD//wD/CgCyCgCyCgCyCgCy/wD//wD//wD//wD/CgCyCgCyCgCyCgCyCgCyCgCy/wD//wD/CgCyCgCyCgCyCgCyCgCyCgCy/wD//wD//wD/CgCyCgCyCgCy" +
        "CgCy/wD//wD//wD/CgCyCgCy/wD//wD/CgCyCgCy/wD//wD/CgCyCgCyCgCyCgCy/wD//wD//wD//wD/CgCyCgCyCgCyCgCy/wD//wD/CgCyCgCy/wD//wD/CgCyCgCy/wD//wD/CgCyCgCy/wD//wD//wD//wD//wD//wD/CgCyCgCy/wD//wD//wD/CgCyCgCy/wD/" +
        "/wD/CgCyCgCy/wD//wD/CgCyCgCy/wD//wD//wD/CgCyCgCyCgCyCgCy/wD//wD//wD/CgCyCgCyCgCyCgCyCgCy/wD//wD//wD//wD/CgCyCgCyCgCyCgCy/wD//wD//wD/CgCyCgCyCgCyCgCyCgCy/wD//wD//wD//wD/CgCyCgCyCgCyCgCy/wD//wD//wD/CgCy" +
        "CgCyCgCyCgCyCgCyCgCy/wD//wD/CgCyCgCy/wD//wD/CgCyCgCy/wD//wD/CgCyCgCy/wD//wD/CgCyCgCy/wD//wD/CgCyCgCy/wD//wD//wD/CgCyCgCy/wD//wD/CgCyCgCy/wD//wD/CgCyCgCy/wD//wD/CgCyCgCy/wD//wD/CgCyCgCy/wD//wD/CgCyCgCy" +
        "CgCyCgCyCgCyCgCy/wD//wD//wD//wD/CgCyCgCy/wD//wD//wD//wD/CgCyCgCy/wD//wD/CgCyCgCy/wD//wD/CgCyCgCy/wD//wD/CgCyCgCy/wD//wD//wD//wD//wD//wD/CgCyCgCy/wD//wD/CgCyCgCy/wD//wD/CgCyCgCy/wD//wD//wD//wD//wD//wD/" +
        "/wD//wD//wD/CgCyCgCy/wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD/CgCyCgCy/wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD/CgCyCgCyCgCy/wD//wD//wD//wD//wD//wD//wD//wD//wD//wD/CgCy" +
        "CgCy/wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD/CgCyCgCy/wD//wD//wD//wD//wD//wD/CgCyCgCyCgCy/wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD/" +
        "/wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD/CgCyCgCy/wD//wD//wD//wD//wD//wD/" +
        "/wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD/CgCyCgCy" +
        "/wD//wD//wD//wD/CgCy/wD//wD/CgCy/wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD/CgCyCgCy/wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD/CgCyCgCyCgCyCgCy/wD//wD//wD//wD//wD/CgCyCgCy/wD//wD//wD/" +
        "/wD//wD/CgCyCgCyCgCyCgCy/wD//wD//wD//wD/CgCyCgCyCgCyCgCy/wD//wD//wD//wD//wD//wD//wD/CgCyCgCy/wD//wD/CgCyCgCyCgCyCgCyCgCyCgCy/wD//wD//wD/CgCyCgCyCgCyCgCy/wD//wD//wD/CgCyCgCyCgCyCgCyCgCyCgCy/wD//wD//wD/" +
        "CgCyCgCyCgCyCgCy/wD//wD//wD//wD/CgCyCgCyCgCyCgCy/wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD/CgCyCgCyCgCyCgCy/wD//wD//wD/CgCyCgCy/wD//wD/CgCyCgCy/wD//wD/CgCyCgCy/wD//wD//wD/CgCyCgCy/wD//wD/CgCy" +
        "CgCy/wD//wD//wD//wD//wD/CgCyCgCy/wD//wD//wD//wD/CgCyCgCy/wD//wD//wD/CgCy/wD//wD//wD/CgCyCgCyCgCyCgCy/wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD/" +
        "/wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD/CgCyCgCyCgCy/wD//wD/CgCyCgCyCgCy/wD//wD//wD//wD//wD//wD//wD/" +
        "CgCyCgCy/wD//wD/CgCyCgCy/wD//wD//wD//wD//wD//wD/CgCyCgCy/wD//wD/CgCyCgCy/wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD/RwCGRwCGRwCGRwCG/wD//wD//wD/RwCGRwCGAAAAAAAARwCGRwCG" +
        "/wD//wD/RwCGRwCGAAAAAAAARwCGRwCG/wD//wD/RwCGRwCGAAAARwCGRwCG/wD//wD//wD/RwCGRwCGAAAAAAAAAAAAAAAAAAAA/wD/RwCGRwCGAAAAAAAAAAAAAAAAAAAA/wD/RwCGRwCGAAAAAAAARwCGRwCG/wD//wD/RwCGRwCGAAAA/wD/RwCGRwCGAAAA/wD/" +
        "/wD/RwCGRwCGAAAAAAAA/wD//wD//wD//wD/RwCGRwCGAAAAAAAA/wD/RwCGRwCGAAAARwCGRwCGAAAAAAAA/wD/RwCGRwCGAAAA/wD//wD//wD//wD//wD/RwCGRwCGRwCG/wD/RwCGRwCGRwCGAAAA/wD/RwCGRwCGRwCG/wD/RwCGRwCGAAAA/wD/RwCGRwCGAAAA" +
        "AAAARwCGRwCG/wD//wD/RwCGRwCGAAAAAAAARwCGRwCG/wD//wD/RwCGRwCGAAAAAAAARwCGRwCG/wD//wD/RwCGRwCGAAAAAAAARwCGRwCG/wD//wD/RwCGRwCGAAAAAAAARwCGRwCG/wD//wD//wD/AAAARwCGRwCGAAAAAAAAAAAA/wD/RwCGRwCGAAAA/wD/RwCG" +
        "RwCGAAAA/wD/RwCGRwCGAAAA/wD/RwCGRwCGAAAA/wD/RwCGRwCGAAAA/wD//wD/RwCGRwCGAAAA/wD/RwCGRwCGAAAA/wD/RwCGRwCGAAAA/wD/RwCGRwCGAAAA/wD/RwCGRwCGAAAA/wD//wD/AAAAAAAAAAAARwCGRwCGAAAA/wD//wD/RwCG/wD/AAAARwCG/wD/" +
        "/wD//wD//wD/AAAAAAAA/wD//wD/AAAAAAAA/wD//wD/AAAAAAAA/wD//wD/AAAAAAAA/wD//wD//wD//wD/RwCG/wD/AAAAAAAA/wD//wD/AAAAAAAA/wD//wD/AAAAAAAA/wD//wD//wD//wD//wD//wD//wD//wD/RwCGRwCGAAAA/wD//wD//wD//wD//wD//wD/" +
        "/wD//wD//wD//wD//wD//wD//wD//wD//wD//wD/RwCGRwCGAAAA/wD//wD//wD//wD//wD//wD//wD//wD//wD//wD/RwCGRwCGAAAAAAAAAAAA/wD//wD//wD//wD//wD//wD//wD//wD//wD/RwCGRwCGAAAA/wD//wD//wD//wD//wD//wD/RwCGRwCG/wD//wD/" +
        "/wD//wD//wD/RwCGRwCG/wD//wD/RwCGRwCGAAAA/wD//wD//wD//wD//wD//wD/RwCGRwCGAAAA/wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD/" +
        "/wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD/RwCGRwCGAAAA/wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD/" +
        "/wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD/AAAAAAAA/wD//wD//wD//wD/AAAA/wD//wD/AAAA/wD//wD//wD/" +
        "RwCG/wD//wD/RwCG/wD//wD//wD//wD//wD//wD/RwCG/wD/AAAAAAAA/wD/RwCGRwCG/wD//wD/RwCGRwCG/wD//wD/RwCGRwCGAAAAAAAARwCGRwCG/wD//wD//wD//wD/RwCGRwCGAAAA/wD//wD//wD/RwCGRwCGAAAAAAAARwCGRwCG/wD//wD/RwCGRwCGAAAA" +
        "AAAARwCGRwCG/wD//wD//wD//wD//wD/RwCGRwCGRwCGAAAA/wD/RwCGRwCGAAAAAAAAAAAAAAAAAAAA/wD/RwCGRwCGAAAAAAAARwCGRwCG/wD//wD/RwCGRwCGAAAAAAAARwCGRwCGAAAA/wD/RwCGRwCGAAAAAAAARwCGRwCG/wD//wD/RwCGRwCGAAAAAAAARwCG" +
        "RwCG/wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD/RwCGRwCGAAAAAAAARwCGRwCG/wD//wD/RwCGRwCGAAAA/wD/RwCGRwCGAAAA/wD/RwCGRwCGAAAA/wD//wD/RwCGRwCGAAAA/wD/RwCGRwCGAAAA/wD//wD//wD/RwCGRwCGRwCGRwCGRwCG/wD//wD/" +
        "RwCGRwCGAAAA/wD/RwCGRwCGAAAA/wD/RwCGRwCGAAAAAAAARwCGRwCG/wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD/RwCGRwCG/wD//wD//wD//wD//wD/RwCGRwCG/wD//wD/RwCGRwCG/wD//wD//wD/RwCGRwCG/wD//wD//wD/RwCGRwCG" +
        "/wD//wD//wD//wD//wD//wD//wD/RwCGRwCG/wD//wD/RwCGRwCG/wD//wD//wD//wD//wD//wD//wD//wD//wD/RwCGRwCGAAAAAAAAAAAA/wD//wD/AAAARwCGRwCG/wD//wD//wD//wD//wD/RwCGRwCGAAAAAAAA/wD//wD/RwCGRwCG/wD//wD//wD//wD/RwCG" +
        "RwCGAAAAAAAA/wD//wD/RwCGRwCG/wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD/ngBGngBGAAAAAAAAngBGngBG/wD//wD/ngBGngBGAAAA/wD/ngBGngBGAAAA/wD/ngBGngBGAAAA/wD//wD/AAAAAAAA/wD/ngBGngBG" +
        "AAAA/wD/ngBGngBG/wD//wD/ngBGngBGAAAA/wD//wD//wD//wD//wD/ngBGngBGAAAA/wD//wD//wD//wD//wD/ngBGngBGAAAA/wD//wD/AAAAAAAA/wD/ngBGngBGAAAA/wD/ngBGngBGAAAA/wD//wD/ngBGngBGAAAA/wD//wD//wD//wD//wD/ngBGngBGAAAA" +
        "/wD//wD/ngBGngBGngBGngBGAAAAAAAA/wD//wD/ngBGngBGAAAA/wD//wD//wD//wD//wD/ngBGngBGngBGngBGngBGngBGngBGAAAA/wD/ngBGngBGngBGngBGngBGngBGAAAA/wD/ngBGngBGAAAA/wD/ngBGngBGAAAA/wD/ngBGngBGAAAA/wD/ngBGngBGAAAA" +
        "/wD/ngBGngBGAAAA/wD/ngBGngBGAAAA/wD/ngBGngBGAAAA/wD/ngBGngBGAAAA/wD/ngBGngBGAAAA/wD//wD/AAAAAAAA/wD//wD//wD/ngBGngBGAAAA/wD//wD//wD/ngBGngBGAAAA/wD/ngBGngBGAAAA/wD/ngBGngBGAAAA/wD/ngBGngBGAAAA/wD/ngBG" +
        "ngBGAAAA/wD//wD/ngBGngBGAAAA/wD//wD/ngBGngBGngBGngBGAAAAAAAA/wD/ngBGngBGAAAA/wD/ngBGngBGAAAA/wD//wD//wD//wD/ngBGngBGAAAAAAAA/wD//wD//wD/ngBGngBG/wD/AAAA/wD//wD//wD//wD/ngBGngBG/wD//wD//wD//wD//wD/ngBG" +
        "ngBGngBGngBG/wD//wD//wD/ngBGngBGngBGngBGngBGngBG/wD//wD/ngBGngBG/wD//wD/ngBGngBG/wD//wD/ngBGngBGngBGngBG/wD//wD//wD/ngBGngBGAAAA/wD//wD//wD//wD//wD//wD/ngBGngBGngBGngBG/wD//wD//wD/ngBGngBGngBGngBGngBG" +
        "AAAA/wD//wD/ngBGngBGngBGngBG/wD//wD//wD/ngBGngBGngBGngBGngBG/wD//wD//wD/ngBGngBGngBGngBGngBG/wD//wD/ngBGngBGAAAA/wD//wD//wD//wD//wD//wD//wD/AAAAAAAA/wD//wD//wD//wD//wD/AAAAAAAA/wD/ngBGngBGAAAA/wD//wD/" +
        "/wD//wD//wD//wD/ngBGngBGAAAA/wD//wD/ngBGngBG/wD//wD/ngBGngBG/wD//wD//wD/ngBGngBGngBGngBGngBG/wD//wD//wD//wD/ngBGngBGngBGngBG/wD//wD//wD/ngBGngBGngBGngBGngBG/wD//wD//wD//wD/ngBGngBGngBGngBGngBG/wD//wD/" +
        "ngBGngBGngBGngBGngBG/wD//wD//wD//wD/ngBGngBGngBGngBGngBG/wD//wD/ngBGngBGngBGngBGngBGngBG/wD//wD/ngBGngBG/wD//wD/ngBGngBG/wD//wD/ngBGngBG/wD//wD/ngBGngBG/wD//wD/ngBGngBG/wD//wD//wD/ngBGngBG/wD//wD/ngBG" +
        "ngBG/wD//wD/ngBGngBG/wD//wD/ngBGngBG/wD//wD/ngBGngBG/wD//wD/ngBGngBGngBGngBGngBGngBG/wD//wD/ngBGngBGngBGngBG/wD//wD//wD/ngBGngBGngBGngBG/wD//wD//wD//wD//wD/AAAA/wD//wD/AAAA/wD//wD//wD/ngBGngBGngBGngBG" +
        "/wD//wD//wD//wD/AAAAAAAA/wD//wD/AAAAAAAA/wD/ngBGngBGAAAAngBGngBGngBGAAAA/wD//wD/ngBGngBGngBGAAAA/wD//wD//wD//wD/AAAAAAAA/wD/ngBGngBGAAAA/wD//wD/AAAAAAAA/wD/ngBGngBGAAAA/wD//wD//wD/ngBGngBGngBGngBGAAAA" +
        "/wD/ngBGngBGngBGngBGngBG/wD//wD//wD/ngBGngBGAAAA/wD//wD/AAAAAAAA/wD//wD/AAAAAAAAngBGngBGAAAAAAAA/wD/ngBGngBGAAAA/wD/ngBGngBGAAAA/wD/ngBGngBGAAAA/wD/ngBGngBGAAAA/wD//wD//wD//wD//wD//wD//wD//wD//wD//wD/" +
        "/wD/AAAAAAAA/wD/ngBGngBGAAAA/wD/ngBGngBGAAAA/wD/ngBGngBGAAAA/wD/ngBGngBGAAAA/wD/ngBGngBGngBGngBGngBGngBGngBGngBG/wD//wD/ngBGngBGAAAAAAAAAAAAAAAAAAAA/wD//wD/AAAAAAAAngBGngBGAAAAAAAA/wD//wD/ngBGngBGngBG" +
        "ngBGAAAAAAAA/wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD/ngBGngBGAAAA/wD//wD//wD//wD//wD/ngBGngBGngBGngBGAAAAAAAA/wD//wD/ngBGngBGAAAA/wD//wD/ngBGngBGAAAA/wD//wD//wD//wD//wD/ngBGngBGAAAAAAAA/wD//wD/" +
        "ngBGngBG/wD//wD//wD//wD//wD//wD//wD/ngBGngBGAAAAAAAA/wD//wD//wD//wD//wD//wD/ngBGngBG/wD//wD//wD/ngBGngBGAAAAAAAA/wD//wD//wD//wD/ngBGngBG/wD//wD/ngBGngBGAAAAAAAA/wD//wD//wD//wD/ngBGngBG/wD//wD/ngBGngBG" +
        "ngBGngBGngBGngBG/wD//wD//wD//wD//wD//wD//wD//wD/6wAP6wAP6wAP6wAP6wAP6wAPAAAA/wD/6wAP6wAP6wAP6wAP6wAPAAAAAAAA/wD/6wAP6wAPAAAA/wD//wD//wD//wD//wD/6wAP6wAPAAAA/wD/6wAP6wAPAAAA/wD/6wAP6wAP6wAP6wAP/wD//wD/" +
        "/wD//wD/6wAP6wAP6wAP6wAP/wD//wD//wD//wD/6wAP6wAPAAAA6wAP6wAP6wAP/wD//wD/6wAP6wAP6wAP6wAP6wAP6wAPAAAA/wD//wD/6wAP6wAPAAAA/wD//wD//wD//wD//wD/6wAP6wAPAAAA/wD//wD/6wAP6wAP6wAPAAAAAAAA/wD//wD//wD/6wAP6wAP" +
        "AAAA/wD//wD//wD//wD//wD/6wAP6wAPAAAA6wAPAAAA6wAP6wAPAAAA/wD/6wAP6wAP6wAP6wAP6wAP6wAPAAAA/wD/6wAP6wAPAAAA/wD/6wAP6wAPAAAA/wD/6wAP6wAP6wAP6wAP6wAPAAAAAAAA/wD/6wAP6wAPAAAA/wD/6wAP6wAPAAAA/wD/6wAP6wAP6wAP" +
        "6wAP6wAPAAAAAAAA/wD//wD/6wAP6wAP6wAP6wAP/wD//wD//wD//wD//wD/6wAP6wAPAAAA/wD//wD//wD/6wAP6wAPAAAA/wD/6wAP6wAPAAAA/wD/6wAP6wAPAAAA/wD/6wAP6wAPAAAA/wD/6wAP6wAPAAAA6wAP/wD/6wAP6wAPAAAA/wD//wD//wD/6wAP6wAP" +
        "AAAAAAAA/wD//wD//wD/6wAP6wAP6wAP6wAPAAAAAAAA/wD//wD//wD/6wAP6wAPAAAAAAAA/wD//wD//wD/6wAP6wAP6wAP6wAP/wD//wD//wD//wD/6wAP6wAP6wAP6wAP/wD//wD//wD/6wAP6wAPAAAAAAAA6wAP6wAP/wD//wD/6wAP6wAPAAAAAAAAAAAAAAAA" +
        "AAAA/wD/6wAP6wAPAAAA/wD/6wAP6wAPAAAA/wD//wD/AAAAAAAA6wAP6wAP/wD//wD/6wAP6wAP6wAP6wAP6wAP/wD//wD//wD/6wAP6wAPAAAAAAAAAAAAAAAA/wD/6wAP6wAPAAAAAAAA6wAP6wAPAAAA/wD/6wAP6wAPAAAAAAAA6wAP6wAP/wD//wD//wD/6wAP" +
        "6wAPAAAAAAAAAAAA/wD/6wAP6wAPAAAAAAAA6wAP6wAPAAAA/wD/6wAP6wAP6wAP6wAP6wAP/wD//wD//wD/6wAP6wAP6wAP/wD//wD//wD//wD//wD/6wAP6wAP/wD//wD/6wAP6wAPAAAA6wAP6wAP/wD//wD//wD//wD/6wAP6wAPAAAA/wD//wD/6wAP6wAP6wAP" +
        "6wAP6wAP6wAP6wAP/wD//wD/6wAP6wAPAAAAAAAA6wAP6wAP/wD//wD/6wAP6wAPAAAAAAAA6wAP6wAP/wD//wD/6wAP6wAPAAAAAAAA6wAP6wAP/wD//wD/6wAP6wAPAAAAAAAA6wAP6wAPAAAA/wD/6wAP6wAPAAAAAAAA6wAP6wAP/wD//wD/6wAP6wAPAAAAAAAA" +
        "AAAAAAAAAAAA/wD//wD/AAAA6wAP6wAPAAAAAAAAAAAA/wD/6wAP6wAPAAAA/wD/6wAP6wAPAAAA/wD/6wAP6wAPAAAA/wD/6wAP6wAPAAAA/wD/6wAP6wAPAAAA6wAP/wD/6wAP6wAPAAAA/wD//wD/6wAP6wAP6wAP6wAPAAAAAAAA/wD/6wAP6wAPAAAA/wD/6wAP" +
        "6wAPAAAA/wD//wD/AAAAAAAA6wAP6wAPAAAAAAAA/wD//wD/AAAAAAAA6wAP6wAP/wD//wD//wD/AAAAAAAA6wAP6wAP/wD//wD//wD/6wAP6wAP6wAP6wAP/wD//wD//wD/6wAP6wAPAAAAAAAA6wAP6wAP/wD//wD/6wAP6wAP/wD//wD/6wAP6wAP/wD//wD/6wAP" +
        "6wAP6wAP/wD/6wAP6wAPAAAA/wD//wD//wD/6wAP6wAPAAAA/wD//wD//wD//wD//wD//wD/6wAP6wAPAAAAAAAA/wD//wD//wD/6wAP6wAP6wAPAAAAAAAA/wD/6wAP6wAP/wD/AAAA6wAP6wAPAAAA/wD//wD/AAAAAAAAAAAA6wAP6wAP/wD//wD/6wAP6wAP6wAP" +
        "6wAP6wAP/wD//wD//wD//wD//wD/6wAP6wAPAAAAAAAA/wD//wD//wD/6wAP6wAP6wAP6wAPAAAAAAAA/wD//wD/6wAP6wAP6wAP6wAP6wAPAAAA/wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD/6wAP6wAPAAAAAAAA/wD/6wAP6wAPAAAA/wD/" +
        "/wD/AAAAAAAA/wD//wD/AAAAAAAA/wD//wD/6wAP6wAPAAAAAAAA6wAP6wAPAAAAAAAA/wD//wD/6wAP6wAP6wAP6wAP/wD//wD//wD//wD//wD/6wAP6wAPAAAAAAAA/wD//wD//wD/6wAP6wAP6wAPAAAAAAAA/wD//wD//wD/6wAP6wAP6wAP6wAP6wAP6wAP/wD/" +
        "/wD/6wAP6wAP6wAP6wAP6wAP6wAP/wD//wD/6wAP6wAP6wAP6wAP6wAP6wAP6wAP6wAP/wD//wD//wD/AAAAAAAA/wD//wD//wD/AAAAAAAA/wD//wD//wD//wD/6wAP6wAPAAAAAAAA/wD//wD//wD//wD/6wAP6wAP/wD//wD//wD//wD//wD/6wAP6wAPAAAAAAAA" +
        "/wD//wD//wD//wD//wD//wD//wD//wD/6wAP6wAP/wD//wD/6wAP6wAPAAAA/wD//wD//wD//wD//wD/6wAP6wAPAAAA/wD//wD/AAAAAAAA/wD//wD//wD//wD//wD//wD/AAAAAAAA/wD//wD/AAAAAAAAAAAAAAAAAAAAAAAA/wD//wD//wD//wD//wD//wD//wD/" +
        "/xYA/xYAAAAAAAAA/xYA/xYAAAAA/wD//xYA/xYAAAAAAAAA/xYA/xYA/wD//wD//xYA/xYAAAAA/wD//wD//wD//wD//wD//xYA/xYAAAAA/wD//xYA/xYAAAAA/wD//xYA/xYAAAAAAAAAAAAA/wD//wD//wD//xYA/xYAAAAAAAAAAAAA/wD//wD//wD//xYA/xYA" +
        "AAAA/wD//xYA/xYAAAAA/wD//xYA/xYAAAAAAAAA/xYA/xYAAAAA/wD//wD//xYA/xYAAAAA/wD//wD//wD//wD//wD//xYA/xYAAAAA/wD//wD//xYA/xYA/xYA/xYA/wD//wD//wD//wD//xYA/xYAAAAA/wD//wD//wD//wD//wD//xYA/xYAAAAA/wD/AAAA/xYA" +
        "/xYAAAAA/wD//xYA/xYAAAAA/xYA/xYA/xYAAAAA/wD//xYA/xYAAAAA/wD//xYA/xYAAAAA/wD//xYA/xYAAAAAAAAAAAAAAAAA/wD//wD//xYA/xYAAAAA/wD//xYA/xYAAAAA/wD//xYA/xYA/xYA/xYAAAAAAAAA/wD//wD//wD//wD/AAAAAAAA/xYA/xYA/wD/" +
        "/wD//wD//wD//xYA/xYAAAAA/wD//wD//wD//xYA/xYAAAAA/wD//xYA/xYAAAAA/wD//xYA/xYAAAAA/wD//xYA/xYAAAAA/wD//xYA/xYA/xYA/xYA/xYA/xYA/xYAAAAA/wD//wD//xYA/xYA/xYA/xYA/wD//wD//wD//wD//wD//xYA/xYAAAAAAAAA/wD//wD/" +
        "/wD//xYA/xYAAAAAAAAA/wD//wD//wD//xYA/xYAAAAAAAAA/xYA/xYA/wD//wD//xYA/xYAAAAAAAAA/xYA/xYA/wD//wD//xYA/xYAAAAA/wD//xYA/xYAAAAA/wD//xYA/xYA/xYA/xYA/wD//wD//wD//wD//xYA/xYAAAAA/wD//xYA/xYAAAAA/wD//xYA/xYA" +
        "/xYA/xYA/xYAAAAA/wD//xYA/xYAAAAAAAAA/xYA/xYA/wD//wD//xYA/xYAAAAA/wD//wD//wD//wD//xYA/xYAAAAA/wD//xYA/xYAAAAA/wD//xYA/xYA/xYA/xYA/xYA/xYAAAAA/wD//wD//xYA/xYAAAAA/wD//wD//wD//xYA/xYAAAAA/wD//xYA/xYAAAAA" +
        "/wD//xYA/xYAAAAAAAAA/xYA/xYA/wD//wD//wD//xYA/xYAAAAA/wD//wD//wD//wD//xYA/xYAAAAA/wD//xYA/xYA/xYA/xYAAAAAAAAA/wD//wD//wD//xYA/xYAAAAA/wD//wD//xYA/xYA/xYA/xYA/xYA/xYA/xYAAAAA/wD//xYA/xYAAAAA/wD//xYA/xYA" +
        "AAAA/wD//xYA/xYAAAAA/wD//xYA/xYAAAAA/wD//xYA/xYAAAAA/wD//xYA/xYAAAAA/wD//xYA/xYAAAAA/wD//xYA/xYAAAAA/wD//xYA/xYAAAAA/wD//wD/AAAAAAAA/wD//wD//xYA/xYA/xYA/xYA/wD//wD//wD//wD//wD//xYA/xYAAAAA/wD//wD//wD/" +
        "/xYA/xYAAAAA/wD//xYA/xYAAAAA/wD//xYA/xYAAAAA/wD//xYA/xYAAAAA/wD//xYA/xYA/xYA/xYA/xYA/xYA/xYAAAAA/wD//wD//wD//xYA/xYAAAAAAAAA/wD//wD//xYA/xYAAAAA/wD//xYA/xYAAAAA/wD//wD//wD//xYA/xYAAAAAAAAA/wD//wD//xYA" +
        "/xYA/xYA/xYA/xYAAAAA/wD//xYA/xYA/xYA/xYA/xYAAAAA/wD//xYA/xYAAAAAAAAA/xYA/xYA/wD//wD//xYA/xYA/xYA/xYA/xYA/xYAAAAA/wD//xYA/xYAAAAA/wD//xYA/xYAAAAA/wD//xYA/xYAAAAAAAAA/xYA/xYAAAAA/wD//wD//wD//xYA/xYAAAAA" +
        "/wD//wD//wD//wD//xYA/xYA/wD/AAAAAAAA/wD//wD//wD//wD//wD/AAAA/xYA/xYA/wD//wD//xYA/xYA/xYA/xYA/xYA/xYA/xYA/wD//wD//wD//wD//wD//xYA/xYAAAAA/wD//xYA/xYAAAAAAAAA/xYA/xYA/wD//wD//wD//wD//xYA/xYAAAAA/wD//wD/" +
        "/wD//xYA/xYAAAAAAAAA/xYA/xYA/wD//wD//wD//wD/AAAAAAAA/xYA/xYAAAAA/wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//xYA/xYAAAAAAAAA/wD//wD//wD/AAAAAAAA/wD//wD//wD//wD//wD//wD//wD//wD//wD//xYA/xYA/xYA/xYA" +
        "/xYA/xYA/xYA/xYA/wD//wD//wD//wD/AAAAAAAA/xYA/xYA/wD//wD//wD//xYA/xYAAAAAAAAA/wD//wD//wD//xYA/xYAAAAAAAAA/xYA/xYA/xYA/wD//wD//wD/AAAAAAAAAAAAAAAAAAAAAAAA/wD//wD/AAAA/xYA/xYAAAAAAAAAAAAA/wD//wD/AAAA/xYA" +
        "/xYA/xYA/xYAAAAAAAAAAAAA/wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//xYA/xYAAAAAAAAA/wD//wD//wD//wD//wD//wD//xYA/xYA/wD//wD//wD//wD//wD//xYA/xYA/wD//wD//wD//wD//wD//wD//wD//wD//xYA/xYAAAAAAAAA/wD/" +
        "/xYA/xYAAAAA/wD//wD//wD//wD//wD//xYA/xYAAAAA/wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//xYA/xYA/xYA/xYA/xYA/xYA/wD//wD//wD//wD//wD//wD//wD//wD//04A/04AAAAA/wD//04A/04AAAAA/wD//04A/04AAAAA/wD/" +
        "/04A/04AAAAA/wD//04A/04AAAAA/wD//04A/04A/wD//wD//04A/04AAAAA/04A/04AAAAAAAAA/wD//04A/04AAAAA/wD//wD//wD//wD//wD//04A/04AAAAA/wD//wD//wD//wD//wD//04A/04AAAAA/wD//04A/04AAAAA/wD//04A/04AAAAA/wD//04A/04A" +
        "AAAA/wD//wD//04A/04AAAAA/wD//wD//04A/04A/wD//04A/04AAAAA/wD//wD//04A/04AAAAA/04A/04A/wD//wD//wD//04A/04AAAAA/wD//wD//wD//wD//wD//04A/04AAAAA/wD//wD//04A/04AAAAA/wD//04A/04AAAAA/wD//04A/04AAAAA/wD//04A" +
        "/04AAAAA/wD//04A/04AAAAA/wD//04A/04AAAAA/wD//wD//wD//wD//wD//wD//04A/04A/04A/04AAAAAAAAA/wD//04A/04AAAAA/04A/04A/wD//wD//wD//04A/04A/wD//wD//04A/04AAAAA/wD//wD//wD//04A/04AAAAA/wD//wD//wD//04A/04AAAAA" +
        "/wD//04A/04AAAAA/wD//wD//04A/04A/04A/04AAAAAAAAA/wD//04A/04A/04AAAAA/04A/04A/04AAAAA/wD//04A/04AAAAAAAAA/04A/04A/wD//wD//wD//wD//04A/04AAAAA/wD//wD//wD//04A/04AAAAAAAAA/wD//wD//wD//wD//04A/04A/04A/04A" +
        "/04A/04AAAAA/wD//04A/04A/04A/04A/04A/04AAAAA/wD//04A/04AAAAA/wD//04A/04AAAAA/wD//04A/04AAAAAAAAAAAAA/wD//wD//wD//04A/04AAAAA/wD//04A/04AAAAA/04A/04AAAAAAAAA/04A/04AAAAA/wD//04A/04AAAAA/wD//04A/04AAAAA" +
        "/wD//04A/04AAAAA/wD//wD//wD//wD//04A/04AAAAA/wD//04A/04AAAAA/wD//04A/04AAAAAAAAAAAAAAAAAAAAA/wD//wD//04A/04AAAAA/wD//wD//wD//wD//04A/04A/04A/04A/04AAAAA/wD//04A/04AAAAA/wD//04A/04AAAAA/wD//wD//04A/04A" +
        "AAAA/wD//wD//wD//wD//04A/04AAAAA/wD//04A/04AAAAA/04A/04A/wD//wD//wD//wD//04A/04AAAAA/wD//wD//04A/04AAAAA/04AAAAA/04A/04AAAAA/wD//04A/04AAAAA/wD//04A/04AAAAA/wD//04A/04AAAAA/wD//04A/04AAAAA/wD//04A/04A" +
        "/04A/04A/04AAAAAAAAA/wD//wD//04A/04A/04A/04A/04AAAAA/wD//04A/04AAAAA/wD//wD//wD//wD//wD//wD//wD/AAAAAAAA/04A/04A/wD//wD//wD//wD//04A/04AAAAA/wD//wD//wD//04A/04AAAAA/wD//04A/04AAAAA/wD//wD//04A/04A/04A" +
        "/04AAAAAAAAA/wD//wD//04A/04A/04A/04A/04AAAAAAAAA/wD//wD//04A/04A/04A/04A/wD//wD//wD//wD//04A/04A/04A/04A/04AAAAA/wD//wD//04A/04AAAAAAAAA/wD//wD//04A/04AAAAAAAAA/04A/04AAAAA/04A/04AAAAAAAAA/04A/04AAAAA" +
        "/wD//04A/04AAAAA/wD//04A/04AAAAA/wD//04A/04AAAAAAAAAAAAAAAAAAAAA/wD//04A/04AAAAA/wD//04A/04AAAAA/wD//04A/04AAAAA/wD//04A/04AAAAA/wD//wD//wD//04A/04AAAAA/wD//wD//wD//04A/04AAAAAAAAA/wD//wD//wD//wD//04A" +
        "/04A/wD//wD//04A/04AAAAA/wD//wD/AAAAAAAAAAAA/04A/04AAAAAAAAA/04A/04A/wD//wD//04A/04AAAAA/wD//04A/04AAAAA/wD//04A/04AAAAA/wD//wD//wD//04A/04AAAAA/wD//wD//wD//04A/04AAAAA/wD//04A/04AAAAA/wD//04A/04A/wD/" +
        "/wD//04A/04AAAAA/wD//04A/04A/wD//wD//wD//04A/04A/wD//wD//wD//wD//wD/AAAAAAAA/wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//04A/04AAAAAAAAA/04A/04AAAAAAAAA/wD//04A/04A/04A/04A/04AAAAA" +
        "AAAA/wD//04A/04AAAAAAAAA/04A/04A/wD//wD//04A/04AAAAA/wD//04A/04AAAAAAAAA/wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//04A/04AAAAA/wD//wD//wD//wD//04A/04AAAAAAAAA/04A/04A/wD//wD//wD//04A/04A/wD//wD//wD/" +
        "/04A/04A/wD//wD//wD//04A/04AAAAAAAAA/wD//wD//wD//wD//wD//wD//wD//wD//04A/04A/wD//wD//wD//wD//wD//04A/04A/wD//wD//wD//wD//wD//wD//04A/04AAAAAAAAA/wD//wD//wD//04A/04A/wD//wD//wD//wD//04A/04AAAAAAAAA/wD/" +
        "/wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD/AAAAAAAAAAAAAAAAAAAAAAAA/wD//wD//wD//wD//wD//wD//wD//5IA/5IAAAAA/wD//5IA/5IAAAAA/wD//5IA/5IA/5IA/5IA/5IAAAAAAAAA/wD//wD//5IA/5IA/5IA/5IAAAAAAAAA/wD/" +
        "/5IA/5IA/5IA/5IAAAAAAAAA/wD//wD//5IA/5IA/5IA/5IA/5IA/5IA/wD//wD//5IA/5IAAAAA/wD//wD//wD//wD//wD//wD//5IA/5IA/5IA/5IAAAAAAAAA/wD//5IA/5IAAAAA/wD//5IA/5IAAAAA/wD//5IA/5IA/5IA/5IA/wD//wD//wD//5IA/5IA/5IA" +
        "AAAAAAAA/wD//wD//5IA/5IAAAAA/wD//5IA/5IA/wD//wD//5IA/5IA/5IA/5IA/5IA/5IA/wD//wD//5IA/5IAAAAA/wD//wD//5IA/5IAAAAA/wD//5IA/5IAAAAA/wD//5IA/5IAAAAA/wD//wD//5IA/5IA/5IA/5IAAAAAAAAA/wD//5IA/5IAAAAA/wD//wD/" +
        "/wD//wD//wD//wD//wD/AAAA/5IA/5IA/5IA/wD//wD//5IA/5IAAAAA/wD//5IA/5IA/wD//wD//wD//5IA/5IA/5IA/5IAAAAAAAAA/wD//wD//wD//5IA/5IAAAAA/wD//wD//wD//wD//5IA/5IA/5IA/5IAAAAAAAAA/wD//wD//wD//5IA/5IAAAAAAAAA/wD/" +
        "/wD//5IA/5IAAAAAAAAA/wD//5IA/5IAAAAA/wD//5IA/5IAAAAA/wD//5IA/5IAAAAA/wD//wD//wD//5IA/5IAAAAA/wD//wD//wD//5IA/5IA/5IA/5IA/5IA/5IA/wD//wD//5IA/5IAAAAAAAAA/5IA/5IAAAAA/wD//5IA/5IAAAAAAAAA/5IA/5IAAAAA/wD/" +
        "/wD//5IA/5IA/5IA/5IAAAAAAAAA/wD//5IA/5IA/5IA/5IA/5IA/5IA/wD//wD//wD//5IA/5IA/5IA/5IAAAAAAAAA/wD//5IA/5IA/5IA/5IA/5IAAAAA/wD//5IA/5IA/5IA/5IA/5IAAAAAAAAA/wD//wD//5IA/5IA/5IA/5IA/wD//wD//wD//5IA/5IA/5IA" +
        "/5IA/5IAAAAA/wD//wD//5IA/5IA/5IA/5IA/wD//wD//wD//wD//5IA/5IAAAAA/wD//wD//wD//wD//wD/AAAAAAAA/5IA/5IAAAAA/wD//5IA/5IAAAAA/wD//5IA/5IAAAAA/wD//5IA/5IA/5IA/5IA/wD//wD//wD//wD//5IA/5IAAAAA/wD//5IA/5IAAAAA" +
        "/wD//5IA/5IA/wD//wD//5IA/5IA/5IA/5IA/wD//wD//5IA/5IAAAAA/wD/AAAA/5IA/5IAAAAA/wD//5IA/5IAAAAA/wD//5IA/5IAAAAA/wD//wD//5IA/5IA/5IA/5IAAAAAAAAA/wD//5IA/5IAAAAAAAAAAAAAAAAA/wD//wD//wD//wD/AAAAAAAA/5IA/5IA" +
        "AAAA/wD//5IA/5IAAAAA/wD//wD//wD//wD//wD//5IA/5IA/5IA/5IA/5IAAAAAAAAA/wD//wD//wD//wD//5IA/5IA/5IA/wD//wD//wD//5IA/5IA/5IA/5IA/5IAAAAA/wD//wD//wD//5IA/5IAAAAAAAAA/wD//wD//wD//5IA/5IAAAAA/5IA/5IAAAAA/wD/" +
        "/wD//5IA/5IAAAAAAAAA/5IA/5IA/wD//wD//wD//wD/AAAA/5IA/5IAAAAAAAAA/wD//5IA/5IA/5IA/5IA/5IA/5IA/wD//wD//5IA/5IA/5IA/5IA/5IAAAAA/wD//5IA/5IA/5IA/5IA/5IAAAAA/wD//wD//5IA/5IA/5IA/5IAAAAAAAAA/wD//wD//5IA/5IA" +
        "/5IA/5IA/wD//wD//wD//wD//5IA/5IA/5IA/5IA/5IAAAAA/wD//wD//5IA/5IA/5IA/5IAAAAAAAAA/wD//5IA/5IA/5IA/5IA/5IA/5IA/wD//wD//5IA/5IA/5IA/5IA/5IA/5IA/wD//wD//wD//5IA/5IA/5IA/5IAAAAAAAAA/wD//wD//wD//wD//wD//5IA" +
        "/5IAAAAA/wD//wD//5IA/5IA/5IA/5IAAAAAAAAA/wD//wD//5IA/5IA/5IA/5IAAAAAAAAA/wD//wD//wD//5IA/5IAAAAA/wD//wD//wD//wD//5IA/5IA/5IA/5IAAAAAAAAA/wD//wD//5IA/5IA/5IA/5IAAAAAAAAA/wD//5IA/5IAAAAA/wD//wD//5IA/5IA" +
        "AAAA/wD//wD//wD//5IA/5IA/wD//wD//wD//wD//5IA/5IA/wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//5IA/5IAAAAA/wD//5IA/5IAAAAA/wD//wD//wD/AAAA/5IA/5IAAAAAAAAA/wD//wD//5IAAAAAAAAA/wD//5IA/5IAAAAA/wD//wD//5IA" +
        "/5IA/5IA/5IA/5IA/5IA/wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD/AAAAAAAA/wD//wD//wD//wD//wD/AAAAAAAA/wD//wD/AAAAAAAA/wD//wD//5IA/5IAAAAA/wD//wD//5IA/5IAAAAA/wD//5IA/5IAAAAAAAAA/wD//wD//wD//wD/" +
        "/wD//wD//wD//wD//wD//wD//5IA/5IA/wD//wD//wD//wD//wD//5IA/5IA/5IA/wD//wD//5IA/5IA/5IAAAAAAAAA/wD//wD//wD//wD//wD//5IA/5IA/wD//wD//5IA/5IAAAAAAAAA/wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD/" +
        "/wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD/AAAAAAAA/wD//wD/AAAAAAAA/wD//wD/AAAAAAAAAAAAAAAAAAAA/wD//wD//wD//wD/AAAAAAAAAAAAAAAA/wD//wD//wD/AAAAAAAAAAAAAAAA/wD//wD//wD//wD/AAAAAAAAAAAA" +
        "AAAAAAAAAAAA/wD//wD/AAAAAAAA/wD//wD//wD//wD//wD//wD//wD/AAAAAAAAAAAAAAAA/wD//wD//wD/AAAAAAAA/wD//wD/AAAAAAAA/wD//wD/AAAAAAAAAAAAAAAA/wD//wD//wD/AAAAAAAAAAAA/wD//wD//wD//wD/AAAAAAAA/wD//wD/AAAAAAAA/wD/" +
        "/wD/AAAAAAAAAAAAAAAAAAAAAAAA/wD//wD/AAAAAAAA/wD//wD//wD/AAAAAAAA/wD//wD/AAAAAAAA/wD//wD/AAAAAAAA/wD//wD//wD/AAAAAAAAAAAAAAAA/wD//wD//wD/AAAAAAAA/wD//wD//wD//wD//wD//wD//wD//wD//wD/AAAAAAAAAAAA/wD//wD/" +
        "AAAAAAAA/wD//wD/AAAAAAAA/wD//wD//wD/AAAAAAAAAAAAAAAA/wD//wD//wD//wD//wD/AAAAAAAA/wD//wD//wD//wD//wD/AAAAAAAAAAAAAAAA/wD//wD//wD//wD//wD/AAAAAAAA/wD//wD//wD//wD/AAAAAAAA/wD//wD//wD/AAAAAAAA/wD//wD/AAAA" +
        "AAAA/wD//wD/AAAAAAAA/wD//wD//wD//wD/AAAAAAAA/wD//wD//wD//wD/AAAAAAAAAAAAAAAAAAAAAAAA/wD//wD/AAAAAAAA/wD//wD/AAAAAAAA/wD//wD/AAAAAAAA/wD//wD/AAAAAAAA/wD//wD//wD/AAAAAAAAAAAAAAAA/wD//wD//wD/AAAAAAAAAAAA" +
        "AAAAAAAAAAAA/wD//wD//wD/AAAAAAAAAAAAAAAA/wD//wD//wD/AAAAAAAAAAAAAAAAAAAA/wD//wD/AAAAAAAAAAAAAAAAAAAA/wD//wD//wD//wD/AAAAAAAAAAAAAAAA/wD//wD//wD/AAAAAAAAAAAAAAAAAAAA/wD//wD//wD/AAAAAAAAAAAAAAAA/wD//wD/" +
        "/wD//wD/AAAAAAAA/wD//wD//wD//9EA/9EA/9EA/9EA/9EAAAAAAAAA/wD//wD/AAAAAAAA/wD//wD/AAAAAAAA/wD//wD/AAAAAAAAAAAAAAAA/9EA/9EA/9EA/9EAAAAAAAAA/wD//wD/AAAAAAAA/wD//wD/AAAAAAAA/wD//wD/AAAAAAAAAAAAAAAA/wD//wD/" +
        "AAAAAAAA/wD//wD//wD/AAAAAAAA/wD//wD/AAAAAAAA/wD//wD/AAAAAAAA/wD//wD//wD/AAAAAAAAAAAAAAAA/wD//wD//9EA/9EAAAAA/wD//wD//wD//wD//wD//wD//wD//wD//wD//9EA/9EAAAAA/wD//wD/AAAAAAAA/wD//wD//wD//wD//wD//wD/AAAA" +
        "AAAAAAAAAAAAAAAA/wD//wD//wD//wD//wD//wD/AAAAAAAAAAAA/wD//wD//wD/AAAAAAAAAAAAAAAAAAAA/wD//wD//wD//wD/AAAAAAAA/wD//wD//wD//wD//wD/AAAAAAAA/wD/AAAAAAAA/wD//wD//wD/AAAAAAAA/wD//wD/AAAAAAAA/wD//9EA/9EA/9EA" +
        "/9EAAAAAAAAA/wD//wD//wD/AAAAAAAAAAAAAAAAAAAAAAAA/wD//wD/AAAAAAAAAAAAAAAAAAAA/wD//wD/AAAAAAAAAAAAAAAAAAAA/wD//wD//wD/AAAAAAAAAAAAAAAA/wD//wD//wD//wD/AAAAAAAAAAAAAAAA/wD//wD//wD//wD/AAAAAAAAAAAAAAAAAAAA" +
        "/wD//wD//wD/AAAAAAAAAAAAAAAA/wD//wD//wD/AAAAAAAAAAAAAAAAAAAAAAAA/wD//wD/AAAAAAAAAAAAAAAAAAAAAAAA/wD//wD//wD/AAAAAAAAAAAAAAAA/wD//wD//wD//wD//wD//wD//wD/AAAAAAAA/wD//wD//wD/AAAAAAAAAAAAAAAA/wD//wD//wD/" +
        "/wD/AAAAAAAAAAAAAAAA/wD//wD//wD//wD//wD/AAAAAAAA/wD//wD//wD//wD//wD/AAAAAAAAAAAAAAAA/wD//wD//wD//wD/AAAAAAAAAAAAAAAA/wD//wD//wD/AAAAAAAA/wD//9EA/9EAAAAAAAAA/wD//wD//wD//wD/AAAAAAAA/wD//wD//wD//wD/AAAA" +
        "AAAA/wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD/AAAAAAAA/wD//wD/AAAAAAAA/wD//wD//wD//wD//wD/AAAAAAAA/wD//wD//wD//wD/AAAA/wD//wD//wD/AAAAAAAA/wD//wD//wD/AAAAAAAAAAAAAAAAAAAAAAAA/wD//wD//wD//wD//wD//wD/" +
        "/wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD/AAAAAAAA/wD//9EA/9EAAAAAAAAA/wD//wD/AAAAAAAA/wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD/AAAAAAAA/wD//wD//wD/" +
        "/wD//wD/AAAAAAAAAAAA/wD//wD/AAAAAAAAAAAA/wD//wD//wD//wD//wD//wD//wD/AAAAAAAA/wD//wD/AAAAAAAA/wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD//wD/";

            #endregion Serialized Image

            Image image = ImageSerializer.Deserialize(serializedImage);
#endif
            // Create the font from the image.
            MultiColorFont font = MultiColorFont.LoadFromImage(
                image,
                " ABCDEFGHIJKLMNOPQRSTUVWXYZÅÄÖÉÜabcdefghijklmnopqrstuvwxyzåäöéü0123456789.,?!\"#$%&-+*:;/\\<>()'`=",
                Color.FromArgb(0xFF, 0xFF, 0x00, 0xFF));

            // Get the characters to scroll.
            IEnumerable<MultiColorCharacter> characters = font.GetChars(_scrollText);

            // Choose a background color (or draw your own more complex background!)
            Color backgroundColor = Color.FromArgb(0xFF, 0x00, 0x20, 0x00);

            // Create the character renderer.
            var characterRenderer = new MultiColorCharacterRenderer();

            // Create the text scroller.
            var textScroller = new TextScroller<MultiColorCharacter>(
                SenseHat.Display,
                characterRenderer,
                characters);

            while (true)
            {
                // Step the scroller.
                if (!textScroller.Step())
                {
                    // Reset the scroller when reaching the end.
                    textScroller.Reset();
                }

                // Clear the display.
                SenseHat.Display.Fill(backgroundColor);

                // Draw the scroll text.
                textScroller.Render();

                // Update the physical display.
                SenseHat.Display.Update();

                // Pause for a short while.
                Sleep(TimeSpan.FromMilliseconds(50));
            }
        }
    }
}
