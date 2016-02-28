# ContinuousReporting

## Synopsis

This project is a graphical way, for a single developer or a team, to trace activities in an application built by Visual Studio Online (Team Fundation Service).

## Motivation

I created this project to help developers on my team. They should know if there commits improved the overall code coverage. From there was established a monthly challenge !
I want to share this project so that it is used, and to get feedback.

## Installation

Create a [Visual Studio Team Services](https://www.visualstudio.com/en-us/products/visual-studio-team-services-pricing-vs.aspx) account. (It's free!).
Create a projet, and setup [Configous Integration](https://www.visualstudio.com/features/continuous-integration-vs).

Configure the Web Site "ContinuousReporting", in the Web.config file.
1) Set the VSO.OrganisationName and VSO.ProjectName settings.
2) Publish the SSDT project to create a schema database in a SqlServer.
3) Set the SQL ConnectionString in the "ReportingDb" connection string.
4) Publish the WebSite anywhere, a free AzureWebSite is just fine.

The web site created, we just have to setup build data source.

1) Create a [Service Hooks](https://www.visualstudio.com/en-us/get-started/integrate/integrating-with-service-hooks-vs), specially a [WebHook](https://www.visualstudio.com/get-started/integrate/service-hooks/webhooks-and-vso-vs).
2) Configure it to POST a JSON via HTTP on "BuildCompleted". The url to configure will be the <PublishedWebSiteLocation>/api/builds

That's all !

## Tests

Some test are included in the solution, others should come soon.

## Contributors

Feel free to improve this project by :
* Adding new statistics to track
* Adding other build sources
* Adding other way to gather data
* ... whatever you want :)

## License

The MIT License (MIT)

Copyright (c) 2016 L3Z4

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.