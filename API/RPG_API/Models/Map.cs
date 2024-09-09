﻿namespace RPG_API.Models
{
    public class Map
    {
        public int Id { get; set; }
        public List<List<int>> Coordinates { get; set; }
        public string ImageUrl { get; set; }
        public Character Character { get; set; }    
        public List<Monster> Monster { get; set; }


        
    }
}
