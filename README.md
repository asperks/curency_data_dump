# curency_data_dump
A snapshot of freely available currency data for trading system testing.  It contains EUR/USD currency pair data from between the dates of 2000 and 2013.

This data was downloaded primarily from gain capital when they freely made the data available for system testing.  The original data, found in .csv files, had many different date formats, and wasn't very consistent.  From about 2000-2004 it's not particularly good at all, and is sporadic at best.  From about 2004 to when the data-set stops, 2013, it gets better.  But it is clearly data that has been sourced from multiple sources, and sometimes it's evident that there are timing issues between the sources.  A few years ago, they stopped offering the service.  The website doesn't exist anymore.

I created a program that processed the data (included  in VB), but it was written almost 10 years ago in VB.Net, because that's what I was using at the time.  It interpolated some of the gaps and inconsistencies throughout the data-set, and smoothed over some of the issues between data that had time mis-matches from several sources.  The code is there, but I haven't really touched the language since, so you can take it or leave it.  It does make the data a little easier to traverse though.

There are no guarantees AT ALL about the veracity of this information.  I didn't create it, and there's no information from where the data was sourced originally.  The interpolated copy that has been developed has even less relevance with any perceived point in time from which the data was originally sourced.  The plus is that it records the turmoil of the GFC, which I found a great method of testing disaster scenarios.  However, the fact is, no-one probably knows where the data was sourced from to begin with.

It's not particularly good quality, so it's really only good for developing processing trading strategies over a large data-set.  If you're looking for more macro trends at a daily and weekly level, the data might be useful.  I'm comfortable releasing it now as it is over five years since I stopped recording it.  So there should be no confusion about how relevant it might be today.  It isn't.  Maybe it never was.  
