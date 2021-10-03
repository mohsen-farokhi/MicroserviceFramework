namespace Framework
{
	public static class String
	{
		static String()
		{
		}

		public static string Fix(string text)
		{
			if (text is null)
			{
				return null;
			}

			text = text.Trim();

			if (text == string.Empty)
			{
				return null;
			}

			while (text.Contains("  "))
			{
				text = text.Replace("  ", " ");
			}

			return text;
		}

	}
}
