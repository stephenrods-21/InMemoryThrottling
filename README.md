# InMemoryThrottling
Rate based Throttling in C# using concurrent dictionary with expiry time.

Caching Implemented using concurrent dictionary with expiry time.
Reading the data directly from CSV file. Note: Please drop the file "hoteldb.csv" under c: drive (C:\hoteldb.csv).
MS Unit test project added to the solution.

In order to handle keys explicitly which are long expired and not requested anymore, Instead of keeping them forever in the dictionary. I have added a timer in Global.asax. Which will run after every 1 minute.
For scenarios such as
If ApiKey "TD100" had request for the first time and then it didn't make any request for next 2 days then I will still be in the dictionary.
