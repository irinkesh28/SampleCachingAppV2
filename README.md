# SampleCachingAppV2
## Enable Caching Based on Provided Filters
Followed the same approch of adding a cache key based on filter query string.
## Implement Cache Invalidation on Code Change
Updated the code and used reflection to identify the logic change inside the given method.
Created a method to get IL byte code string, and append the same to cache key. So that when any method code change it will generate a new cache.
## Propose and Implement Dynamic Filtering, Sorting, and Pagination
Simplified the code by using OData as suggested. Earlier I've used raw sql query just to support nested and complex filter conditions.
