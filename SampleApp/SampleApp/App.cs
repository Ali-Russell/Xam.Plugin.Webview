﻿using System;
using System.Diagnostics;
using Xam.Plugin.Abstractions;
using Xam.Plugin.Abstractions.Enumerations;
using Xam.Plugin.Abstractions.Events.Inbound;
using Xamarin.Forms;

namespace SampleApp
{
    public class App : Application
    {

        private FormsWebView WebView;

        public App()
        {

            WebView = new FormsWebView()
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                ContentType = WebViewContentType.Internet,
                Source = "https://www.xamarin.com"
            };

            /*
             * Uncomment to use HTML data
            WebView = new FormsWebView()
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                ContentType = WebViewContentType.StringData,
                Source = "<html><body><h1>Hello World!</h1></body></html>"
            };
            */

            /*
             * Uncomment to use local file
            WebView = new FormsWebView()
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                ContentType = WebViewContentType.LocalFile,
                Source = "Sample.html"
            };
            */

            WebView.RegisterCallback("test", (str) =>
            {
                Debug.WriteLine(str);
            });

            // The root page of your application
            var content = new ContentPage
            {
                Title = "SampleApp",
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    BackgroundColor = Color.Red,
                    Children = {
                        WebView
                    }
                }
            };

            WebView.OnNavigationStarted += OnNavigationStarted;
            WebView.OnNavigationCompleted += OnNavigationComplete;
            WebView.OnContentLoaded += OnContentLoaded;
            WebView.OnJavascriptResponse += OnJavascriptResponse;

            content.Appearing += Content_Appearing;
            MainPage = new NavigationPage(content);
        }

        private void OnContentLoaded(WebView.Plugin.Abstractions.Events.Inbound.ContentLoadedDelegate eventObj)
        {
            Debug.WriteLine(string.Format("Load Complete: {0}", eventObj.Sender.Source));
            eventObj.Sender.InjectJavascript("csharp('Testing');");
            eventObj.Sender.InjectJavascript("alert('Demonstration Alert');");
        }

        private void OnJavascriptResponse(JavascriptResponseDelegate eventObj)
        {
            Debug.WriteLine(string.Format("Javascript: {0}", eventObj.Data));
        }

        /// <summary>
        /// To return a string to c#, simple invoke the csharp(str) method.
        /// </summary>
        private void OnNavigationComplete(NavigationCompletedDelegate eventObj)
        {
            Debug.WriteLine(string.Format("Navigation has been commited: {0}", eventObj.Uri));
        }

        /// <summary>
        /// You can cancel a URL from being loaded by returning a delegate with the cancel boolean set to true.
        /// </summary>
        private NavigationRequestedDelegate OnNavigationStarted(NavigationRequestedDelegate eventObj)
        {
            if (eventObj.Uri == "www.somebadwebsite.com")
                eventObj.Cancel = true;

            return eventObj;
        }

        private void Content_Appearing(object sender, EventArgs e)
        {
            //WebView.Uri = "https://www.google.com"
            //WebView.Uri = "<html><body>Hello World!</body></html>";
            //WebView.Uri = "Web/Sample.html";
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
