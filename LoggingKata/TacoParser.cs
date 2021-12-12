namespace LoggingKata
{
    public class TacoParser
    {
        readonly ILog logger = new TacoLogger();
        
        public ITrackable Parse(string line)
        {
            logger.LogInfo("Begin parsing");

            var cells = line.Split(',');

            if (cells.Length < 3)
            {
                logger.LogInfo("Cells Are Less Than 3 .. Parsing Failed/Incorrect Input to CVS");
                
                return null;
            }

            var latitude = double.Parse(cells[0]);
          
            var longitude = double.Parse(cells[1]);
          
            var name = cells[2];

            var tacoBell = new TacoBell(latitude, longitude, name);

            return tacoBell;
        }
    }
}