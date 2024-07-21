namespace PunctualSolutions.Dance.Dance
{
    public class DanceRequest
    {
        public DanceRequest(Dancer dancer) => Dancer = dancer;
        public Dancer Dancer { get; private set; }
    }
}