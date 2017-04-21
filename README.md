# Elmah.Io.Sitecore.log4net

Log to elmah.io from the default Sitecore log4net implementation.

## Installation

First, make sure that you have installed the [elmah.io NuGet package](https://www.nuget.org/packages/elmah.io/) into your solution, and that you have added all of the necessary configuration to the Sitecore Web.config file. 

> For more information on how to install elmah.io, please refer to the section [Configure elmah.io manually](https://docs.elmah.io/configure-elmah-io-manually/) found on [elmah.io documentation](https://docs.elmah.io/).

Once installed, you need to configure your elmah.io log ID in the configuration file, found under ``App_Config/Include/Elmah.io.Sitecore.log4net.config``.