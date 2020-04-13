using Amazon.Lambda.Core;
using Saper.Game;
using Saper.Model;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace Saper
{
    public class Function
    {
        public Map FunctionHandler(Model model)
        {
            return new MapGenerator(new BombGenerator(), new HintsGenerator()).GenerateMap(model.amountOfBombs, model.width, model.height);
        }

        public class Model
        {
            public int height;
            public int width;
            public int amountOfBombs;
        }
    }
}
