using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace MsolapWrapper
{
	// Token: 0x02000093 RID: 147
	internal static class OnPremiseServiceErrorExtractor
	{
		// Token: 0x060001E1 RID: 481 RVA: 0x0000949C File Offset: 0x0000889C
		public static WrapperErrorSource ExtractSource(string message, out string onPremErrorCode, out WrapperErrorSourceOrigin errorSourceOrigin)
		{
			errorSourceOrigin = WrapperErrorSourceOrigin.MsolapWrapper;
			onPremErrorCode = null;
			if (string.IsNullOrEmpty(message))
			{
				return WrapperErrorSource.Unknown;
			}
			OnPremiseServiceExceptionPayload onPremiseServiceExceptionPayload = null;
			try
			{
				DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(OnPremiseServiceExceptionPayload));
				MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(message));
				onPremiseServiceExceptionPayload = (OnPremiseServiceExceptionPayload)dataContractJsonSerializer.ReadObject(memoryStream);
			}
			catch (Exception)
			{
				onPremiseServiceExceptionPayload = null;
			}
			if (null != onPremiseServiceExceptionPayload)
			{
				PowerBIApiErrorObject error = onPremiseServiceExceptionPayload.Error;
				if (null != error)
				{
					onPremErrorCode = error.Code;
					if (null != onPremiseServiceExceptionPayload.Error.PowerBIErrorDetails)
					{
						errorSourceOrigin = WrapperErrorSourceOrigin.OnPremise;
						int exceptionCulprit = onPremiseServiceExceptionPayload.Error.PowerBIErrorDetails.ExceptionCulprit;
						WrapperErrorSource wrapperErrorSource;
						if (exceptionCulprit != 0)
						{
							if (exceptionCulprit != 1)
							{
								wrapperErrorSource = ((exceptionCulprit != 2) ? WrapperErrorSource.Unknown : WrapperErrorSource.External);
							}
							else
							{
								wrapperErrorSource = WrapperErrorSource.User;
							}
						}
						else
						{
							wrapperErrorSource = WrapperErrorSource.PowerBI;
						}
						return wrapperErrorSource;
					}
				}
			}
			return WrapperErrorSource.Unknown;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00009460 File Offset: 0x00008860
		private static WrapperErrorSource ConvertExceptionCulprit(int exceptionCulprit)
		{
			if (exceptionCulprit == 0)
			{
				return WrapperErrorSource.PowerBI;
			}
			if (exceptionCulprit != 1)
			{
				return (exceptionCulprit != 2) ? WrapperErrorSource.Unknown : WrapperErrorSource.External;
			}
			return WrapperErrorSource.User;
		}
	}
}
