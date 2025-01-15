using System;
using System.Runtime.Serialization;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal
{
	// Token: 0x0200014C RID: 332
	internal class DaxTranslationException : CommandTreeTranslationException
	{
		// Token: 0x0600127B RID: 4731 RVA: 0x00035788 File Offset: 0x00033988
		internal DaxTranslationException()
		{
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x00035790 File Offset: 0x00033990
		internal DaxTranslationException(string message)
			: base(message)
		{
		}

		// Token: 0x0600127D RID: 4733 RVA: 0x00035799 File Offset: 0x00033999
		internal DaxTranslationException(string message, CommandTreeTranslationErrorCode errorCode)
			: base(message, errorCode)
		{
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x000357A3 File Offset: 0x000339A3
		internal DaxTranslationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x0600127F RID: 4735 RVA: 0x000357AD File Offset: 0x000339AD
		protected DaxTranslationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
