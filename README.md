

# Cryptical
A Windows Universal Platform App to view the latest cryptocurrency exchange rates, news and businesses that accept cryptocurrency in Ireland.

COVER IMAGE

Cryptical, the fun way to dabble in the world of all things cryptocurrency.

Want the latest values of the hottest Cryptocurrencies? Ever tried to look up the latest Bitcoin value and been bombarded with graphs and whacky figures dating back to its creation?

You're not alone. Cryptical is designed with clarity at its core. Presenting a quirky, fun - but most importantly, user-focused experience. Don't get bogged down with heavy figures and cryptic values. Get what's critical - Get Cryptical.

# Contents
	1. App Design & Code 
	2. User Interface
	3. Requirements
	4. Development


IMAGE oF OVERVIEW
# App Design & Code
## Splash Screen
Following the Microsoft documentation on splashscreen generation, the default splashscreen was changed to the custom Cryptical page. This was achieved by first extending the default load page to the extended screen. 

An animation was then added for a smoother UX as the app loads in the background. The animation was created using the [LottieAnimations Package](https://www.nuget.org/packages/LottieUWP/) available on NuGet. The colours of the animation were adapted to fit with the colour scheme of the app.

GIF OF START UP

## Currency Values
The 



## Bitcoin Friendly Businesses
## Latest News




# User Interface

Creating a clear UI was of the utmost importance in the design of Cryptical.  The layout of the app is adptive and responsive from top to toe. This was achieved through the use of  adaptive XAML components. The AdaptiveGridView Control presents items in a evenly-spaced set of columns to fill the total available display space. It reacts to changes in the layout as well as the content so it can adapt to different form factors automatically.

 *This can be viewed on the NewsPage where the content &layout will adapt dynamically with page resizing*.
ADD GIF HERE OF SQUISHING APP

### Colour Scheme
Cryptical is marketed as a simple and fun app to guide & introduce users to the world of cryptocurrencies.  In keeping with this, a bright and cheerful colour scheme was chosen for the app. 
IMAGE OF COLOUR SCHEME
From the app logo to the default colours, this colour scheme is consistent throughout Cryptical. 

Guidance on Windows Universal App design and example code was adapted from from [Microsoft developer's website](https://developer.microsoft.com/en-us/windows/apps/design) . 

# Multilanguage Support
Cryptical has full support in both the English and Irrish language. As a native Irish and English speaker it proved a great opportunity to learn about [String Localisation](https://docs.microsoft.com/en-us/windows/uwp/app-resources/localize-strings-ui-manifest). 

- All applicaton data provided in both lanuguages respectively. (*news articles are in english as no Irish sources are currently available*)
- Privacy policies for the application are provided in both languages.
- Bing map's place names are automatically translated

# Development
In the beginning of developing the application, I considered using templates such as the popular [Template 10 Package](https://github.com/Windows-XAML/Template10/wiki) or the [Windows Template Studio](https://marketplace.visualstudio.com/items?itemName=WASTeamAccount.WindowsTemplateStudio) provided by Microsoft. I researched their implementation, however did not use any templates in an effort to maximise the learing  outcomes of this project. 

I initially aimed to support the [locations.txt]() file, holding all the geolocations of bitcoin friendly locations, in a remote server. I atempted to use the Azure Mobile Storage however this proved more complicated than expected with CosmosDB. I also researched using the popular Firebase DBaas, however, it does not support UWP. 

# Requirements
### Extra Features
