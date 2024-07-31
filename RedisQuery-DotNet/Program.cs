using StackExchange.Redis;
using System;

class Program
{
    static void Main(string[] args)
    {
        // Configuration
        // Retrieve configuration from environment variables
        string redisEndpoint = Environment.GetEnvironmentVariable("REDIS_ENDPOINT");
        string redisPassword = Environment.GetEnvironmentVariable("REDIS_PASSWORD");
        string redisConnectionString = $"{redisEndpoint},ssl=true,user=readwrite,password={redisPassword},abortConnect=false";

        try
        {
            // Create a connection to Redis
            var connection = ConnectionMultiplexer.Connect(redisConnectionString);
            IDatabase db = connection.GetDatabase();

            // Test set and get
            string testKey = "testkey";
            string testValue = "Hello Redis!";
            db.StringSet(testKey, testValue);
            string getValue = db.StringGet(testKey);

            Console.WriteLine($"Stored: {testValue}, Retrieved: {getValue}");

            // Successful retrieval
            if (getValue == testValue)
            {
                Console.WriteLine("Redis connection successful and data verified!");
            }
            else
            {
                Console.WriteLine("Data mismatch, check Redis setup.");
            }
        }
        catch (RedisConnectionException ex)
        {
            Console.WriteLine($"Redis connection failed: {ex.Message}");
        }
    
    }
}
