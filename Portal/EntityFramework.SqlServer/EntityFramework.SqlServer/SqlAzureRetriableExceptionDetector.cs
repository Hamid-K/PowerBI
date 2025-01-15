using System;
using System.Data.SqlClient;

namespace System.Data.Entity.SqlServer
{
	// Token: 0x0200000B RID: 11
	internal static class SqlAzureRetriableExceptionDetector
	{
		// Token: 0x0600003E RID: 62 RVA: 0x00002578 File Offset: 0x00000778
		public static bool ShouldRetryOn(Exception ex)
		{
			SqlException ex2 = ex as SqlException;
			if (ex2 != null)
			{
				foreach (object obj in ex2.Errors)
				{
					int number = ((SqlError)obj).Number;
					if (number <= 10929)
					{
						if (number <= 233)
						{
							if (number <= 64)
							{
								if (number != 20 && number != 64)
								{
									continue;
								}
							}
							else if (number != 121 && number != 233)
							{
								continue;
							}
						}
						else if (number <= 10054)
						{
							if (number != 1205 && number - 10053 > 1)
							{
								continue;
							}
						}
						else if (number != 10060 && number - 10928 > 1)
						{
							continue;
						}
					}
					else if (number <= 41302)
					{
						if (number <= 40501)
						{
							if (number != 40197 && number != 40501)
							{
								continue;
							}
						}
						else if (number != 40613 && number - 41301 > 1)
						{
							continue;
						}
					}
					else if (number <= 41325)
					{
						if (number != 41305 && number != 41325)
						{
							continue;
						}
					}
					else if (number != 41839 && number - 49918 > 2)
					{
						continue;
					}
					return true;
				}
				return false;
			}
			return ex is TimeoutException;
		}
	}
}
