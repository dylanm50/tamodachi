using System;
using System.Collections.Generic;
using System.Text;

namespace tamodachi
{
    public class FoodRelationship
    {
        public Food food { get; }
        public int like { get; }

        public FoodRelationship(Global.FoodNames name, Tamodachi person)
        {   
            Food food = Global.FoodNameToFood(name);

            this.food = food;

            Random random = new Random();

            if (person.Personality.name == food.personality)
            {
                like = random.Next(5, 10);
            }else if (person.Personality.name == Global.PersonalityNames.contempt)
            {
                like = random.Next(5, 6);
            }
            else
            {
                like = random.Next(1, 5);
            }
        }

        public FoodRelationship(Global.FoodNames name, int like)
        {
            food = Global.FoodNameToFood(name);
            this.like = like;
        }
    }
}
