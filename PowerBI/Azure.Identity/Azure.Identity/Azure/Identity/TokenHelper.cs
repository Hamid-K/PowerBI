using System;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Azure.Core;

namespace Azure.Identity
{
	// Token: 0x02000089 RID: 137
	internal static class TokenHelper
	{
		// Token: 0x06000483 RID: 1155 RVA: 0x0000DDB8 File Offset: 0x0000BFB8
		[return: TupleElementNames(new string[] { "ClientId", "TenantId", "Upn", "ObjectId" })]
		public static ValueTuple<string, string, string, string> ParseAccountInfoFromToken(string token)
		{
			Argument.AssertNotNullOrEmpty(token, "token");
			string[] array = token.Split(new char[] { '.' });
			if (array.Length != 3)
			{
				throw new ArgumentException("Invalid token", "token");
			}
			ValueTuple<string, string, string, string> valueTuple = default(ValueTuple<string, string, string, string>);
			try
			{
				string text = array[1].Replace('_', '/').Replace('-', '+');
				int num = array[1].Length % 4;
				if (num != 2)
				{
					if (num == 3)
					{
						text += "=";
					}
				}
				else
				{
					text += "==";
				}
				Utf8JsonReader utf8JsonReader;
				utf8JsonReader..ctor(Convert.FromBase64String(text), default(JsonReaderOptions));
				while (utf8JsonReader.Read())
				{
					if (utf8JsonReader.TokenType == 5)
					{
						string @string = utf8JsonReader.GetString();
						if (!(@string == "appid"))
						{
							if (!(@string == "tid"))
							{
								if (!(@string == "upn"))
								{
									if (!(@string == "oid"))
									{
										utf8JsonReader.Read();
									}
									else
									{
										utf8JsonReader.Read();
										valueTuple.Item4 = utf8JsonReader.GetString();
									}
								}
								else
								{
									utf8JsonReader.Read();
									valueTuple.Item3 = utf8JsonReader.GetString();
								}
							}
							else
							{
								utf8JsonReader.Read();
								valueTuple.Item2 = utf8JsonReader.GetString();
							}
						}
						else
						{
							utf8JsonReader.Read();
							valueTuple.Item1 = utf8JsonReader.GetString();
						}
					}
				}
			}
			catch
			{
				AzureIdentityEventSource.Singleton.UnableToParseAccountDetailsFromToken();
			}
			return valueTuple;
		}
	}
}
