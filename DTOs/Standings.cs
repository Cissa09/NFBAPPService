namespace NFB.DTOs
{
    public class Standings
    {
        public int RosterId { get; set; }
        public string OwnerId { get; set; }
        public string OwnerName { get; set; } 
        public int Wins { get; set; }
        public int Losses { get; set; }
        public double PointsFor { get; set; }
        public int StandingPosition { get; set; }
}
