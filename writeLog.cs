   public void WriteParseLog(string logInfo)
        {
            var currentErrors = DateTime.Now.ToString() + " | " + logInfo;
            string currentFileName = "ParsingLog_" + DateTime.Now.ToShortDateString() + ".log";

            try
            {
                using (StreamWriter sw = new StreamWriter(currentFileName, true, System.Text.Encoding.Default))
                {
                    sw.WriteLine(currentErrors);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during logging");
                var nameFile = "LoggingErrors.log";
                string writeError = DateTime.Now.ToString() + " | " + ex.Message + " | " + ex.StackTrace;

                using (StreamWriter sw = new StreamWriter(nameFile, true, System.Text.Encoding.Default))
                {
                    sw.WriteLine(writeError);
                }
            }
        }

	WriteLine("Pizdec Tak zhit' be SOLID +++++++");

		//ха-ха биба
        //010101001010001010010

	WriteLine("Poshli pit' chai");