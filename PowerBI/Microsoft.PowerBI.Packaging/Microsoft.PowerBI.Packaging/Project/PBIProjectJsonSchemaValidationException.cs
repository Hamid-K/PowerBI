using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Schema;

namespace Microsoft.PowerBI.Packaging.Project
{
	// Token: 0x02000071 RID: 113
	[Serializable]
	public class PBIProjectJsonSchemaValidationException : PBIProjectException
	{
		// Token: 0x0600030D RID: 781 RVA: 0x000089D6 File Offset: 0x00006BD6
		public PBIProjectJsonSchemaValidationException(string message, string artifactName, IList<ValidationError> errors, PBIProjectException.PBIProjectErrorCode errorCode)
			: base(message, PBIProjectJsonSchemaValidationException.GetAnonymizedErrorDescription(artifactName, errors), errorCode, null, null)
		{
		}

		// Token: 0x0600030E RID: 782 RVA: 0x000089EC File Offset: 0x00006BEC
		private static string GetAnonymizedErrorDescription(string artifactName, IList<ValidationError> errors)
		{
			return "EnsureJObjIsPerSchema Error: " + artifactName + " ValidationErrorCodes: " + string.Join(", ", errors.Select((ValidationError err) => err.ErrorType.ToString()));
		}
	}
}
