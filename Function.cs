using Amazon.Lambda.Core;
using Saper.Game;
using Saper.Model;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AWSLambda1
{
    public class Function
    {
        public Map FunctionHandler()
        {
            return new MapGenerator(new BombGenerator(), new HintsGenerator()).GenerateMap(8, 6, 6);
        }
    }
}
