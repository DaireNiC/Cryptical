


# Cryptical
A Windows Universal Platform App to view the latest cryptocurrency exchange rates, news and businesses that accept cryptocurrency in Ireland.

![cover](https://github.com/DaireNiC/Cryptical/blob/master/media/cover_logo.png)


> Cryptical, the fun way to dabble in the world of all things
> cryptocurrency. Want the latest values of the hottest
> Cryptocurrencies? Ever tried to look up the latest Bitcoin value and
> been bombarded with graphs and whacky figures dating back to its
> creation? You're not alone. Cryptical is designed with clarity at its
> core. Presenting a quirky, fun - but most importantly, user-focused
> experience. Don't get bogged down with heavy figures and cryptic
> values. Get what's critical - Get Cryptical.


# Contents
	1. App Design & Code 
	2. User Interface
	3. Development
	4. Testing & Certification
	5. Issues Encountered


![overview](https://github.com/DaireNiC/Cryptical/blob/master/media/app_overview.png)



# App Design & Code

## Navigation
Initially the [Hamburger Component](https://docs.microsoft.com/en-us/windows/uwpcommunitytoolkit/controls/hamburgermenu) was used to provide navigation throughout the app. However, as this component is in the process of being deprecated, I switched to using the [NavigationView Control](https://docs.microsoft.com/en-us/windows/uwp/design/controls-and-patterns/navigationview) instead. The code for navigation can be found in the [MainPage.cs](https://github.com/DaireNiC/Cryptical/blob/master/Cryptical/Views/MainPage.xaml.cs) file.
## Splash Screen
Following the Microsoft documentation on splashscreen generation, the default splashscreen was changed to the custom Cryptical page. This was achieved by first extending the default load page to the extended screen. 

An animation was then added for a smoother UX as the app loads in the background. The animation was created using the [LottieAnimations Package](https://www.nuget.org/packages/LottieUWP/) available on NuGet. The colours of the animation were adapted to fit with the colour scheme of the app.

## Currency Values
The main page of the application is the Market Values Page. This page pulls the latest information on Bitcoin, Litecoin, Ethereum & Ripple values. The [CryptoCompare API](https://www.cryptocompare.com/) was used to fetch this data. 

Error Handling is covered in the case of loss of internet connection as shown below. 
![error](https://github.com/DaireNiC/Cryptical/blob/master/media/error.JPG)

This was achieved using the [Message Dialogue Component](https://docs.microsoft.com/en-us/windows/uwp/design/controls-and-patterns/dialogs) which allows the user to resend the API request  or ignore and continue using the application.


## Bitcoin Friendly Businesses
This page provides users with a map of businesses in Ireland that accept Bitcoin as payment. 

The user's geolocation is used to present their position on the map in relation to to these Businesses/POI. A custom pin representing the Bitcoin logo is used instead of the default to add a customized feel to the map. 

To allow for cleaner code, part of the geolocation logic is found in the LocationsService.cs file. The geolocations are provided in [**local storage**](https://docs.microsoft.com/en-us/windows/uwp/files/quickstart-reading-and-writing-files). Once the page is loaded, the locations are loaded from storage and drawn on the map.


## Latest News
The News page allows users to view the latest news in relation to Bitcoin/cryptocurrencies. The [News.org ](https://newsapi.org/) API, in conjunction with the [Hassie  NewsAPI.NET Wrapper](https://github.com/hassie-dash/NewsAPI.NET/tree/master/Hassie.API.NewsAPI) was used to pull the latest news articles. 

Articles are displayed is a responsive, adaptive card-like fashion. All news articles are clickable. Doing so will bring the user to the source website to read the article in full.


# User Interface

Creating a clear UI was of the utmost importance in the design of Cryptical.  The layout of the app is adptive and responsive from top to toe. This was achieved through the use of  adaptive XAML components. The AdaptiveGridView Control presents items in a evenly-spaced set of columns to fill the total available display space. It reacts to changes in the layout as well as the content, allowing for adaption to different form factors automatically.

 *This can be viewed on the NewsPage where the content & layout will adapt dynamically with page resizing*.


### Colour Scheme
Cryptical is marketed as a simple and fun app to guide & introduce users to the world of cryptocurrencies.  In keeping with this, a bright and cheerful colour scheme was chosen for the app. 

![colors](https://github.com/DaireNiC/Cryptical/blob/master/media/colour_scheme.JPG)

From the app logo to the default colours, this colour scheme is consistent throughout Cryptical. 

## Fluent design
Microsoft's Fluent design system was also incorporated into the app through the use of acrylics. This is most evident in the menu bar when it overlaps content. The densely translucent menu lets the background and windows behind the current focus blur through.

Guidance on Windows Universal App design and example code was adapted from from [Microsoft developer's website](https://developer.microsoft.com/en-us/windows/apps/design) . 

# Multilanguage Support
Cryptical has full support in both the English and Irrish language. As a native Irish and English speaker it proved a great opportunity to learn about [String Localisation](https://docs.microsoft.com/en-us/windows/uwp/app-resources/localize-strings-ui-manifest). 

- All applicaton data provided in both lanuguages respectively. (*news articles are in english as no Irish sources are currently available*)
- Privacy policies for the application are provided in both languages.
- Bing map's place names are automatically translated

# Development
In the beginning of developing the application, I considered using templates such as the popular [Template 10 Package](https://github.com/Windows-XAML/Template10/wiki) or the [Windows Template Studio](https://marketplace.visualstudio.com/items?itemName=WASTeamAccount.WindowsTemplateStudio) provided by Microsoft. I researched their implementation, however did not use any templates in an effort to maximise the learing outcomes of this project. 

I initially aimed to support the [locations.txt](https://github.com/DaireNiC/Cryptical/blob/master/cryptical/Data/locations.txt) file, holding all the geolocations of bitcoin friendly locations, in a remote server. I atempted to use the Azure Mobile Storage however this proved more complicated than expected with CosmosDB. I also researched using the popular DBaas Firebase , however, it does not support UWP. 

# Testing & Certification 
Cryptical is available on the [Windows App store](https://www.microsoft.com/ga-ie/store/p/cryptical/9nlgvsrqd3mv?rtc=1#)! 
Publisher Name: Daire Ní Chatháin
Developed on Visual Studio 2015

#  Issues Encountered 
## News API Error
 -  News API error when creating request. Recieved  the following error:
 - `News API HTTP Exception - parametersMissing: Required parameters are missing, the scope of your search is too broad. Please set any of the following required parameters and try again: q, sources, domains.` 
 - I posted an isssue on the NewsApi.Net Wrapper Github [here](https://github.com/hassie-dash/NewsAPI.NET/issues/1).
 - Turned out to be a bug in their code, which they subsequently fixed and my request then worked. 
## Microsoft Devloper Account Creation
 - Recieved an error on every attempt to create a developer account with Student Promo code. 
 - Microsoft Support Team verified it was a global issue.
 - 2 weeks from first support request, issue was resolved. However this resulted in a delay of my submission to the Windows app Store. 
## Git Upload
 - After application was packaged for the Windows store, I tried to push latest local commits.
 - Package generation resulted in the local repo being 1.5GB in size & git refusing to push due to size.
 - Deleted uneccesary files and retried push but deleted files were still tracked.
 - Created a .gitignore file, removed all tracked files, added all files again &  issue remains unresolved.
