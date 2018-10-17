# Coukkas Api
Web api in ASP.NET Core 2.0.

Users can catch promotional coupons (which in the app have been called "coukkas"), when they are in the right place and at the right time

Examples of endpoints:

http://www.coukkas.com.pl/account/login - execution of the http post command on this address with the application of the email and password in JSON format, enables logging in to the user in the application. After authentication, a response from the so-called token - a sequence of characters in which information about the user is encoded. The token enables user authorizations in the application - it consists in checking whether the given operation is available to the user, for example: creation
the area with coupons is only possible for the business user.

http://www.coukkas.com.pl/fence/create - entering the parameters of the area to this address via the http post command allows you to create a region for promotional coupons. The function is available to a logged-in business users authorized to create areas.

http://www.coukkas.com.pl/fence/infences - at this address user (after sharing his location) gets information about the areas in which he is currently located. He is informed about the name of the area, the description of the promotion and the number of currently available coupons

http://www.coukkas.com.pl/fence/outfences - returns a list of the 10 nearest regions not involving the user. The name of the region and its description are displayed, as well as the distance to the border of the area (in meters or km). If the user is interested in the offer, he must find the region, changing his location and monitoring the displayed distance to the area.

Main endpoint for machine learning - enter the year and range of days in the year from which we want to see the data. For example:
http://www.coukkas.com.pl/account/factsdata_year=2017_days=200-202
will display all attempts of catching coupons on July 20, 2017. 
