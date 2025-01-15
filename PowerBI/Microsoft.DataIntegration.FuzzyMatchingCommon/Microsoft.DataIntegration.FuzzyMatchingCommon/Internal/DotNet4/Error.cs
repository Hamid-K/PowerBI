using System;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Internal.DotNet4
{
	// Token: 0x02000034 RID: 52
	internal static class Error
	{
		// Token: 0x0600018A RID: 394 RVA: 0x00011484 File Offset: 0x0000F684
		internal static Exception Argument(string message)
		{
			return new ArgumentException(message);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0001148C File Offset: 0x0000F68C
		internal static Exception ArgumentNull(string parameterName)
		{
			return new ArgumentNullException(parameterName);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00011494 File Offset: 0x0000F694
		internal static Exception ArgumentOutOfRange(string parameterName)
		{
			return new ArgumentOutOfRangeException(parameterName);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0001149C File Offset: 0x0000F69C
		internal static Exception InvalidOperation(string message)
		{
			return new InvalidOperationException(message);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x000114A4 File Offset: 0x0000F6A4
		internal static Exception NoElements()
		{
			return new InvalidOperationException("Sequence contains no elements");
		}

		// Token: 0x0600018F RID: 399 RVA: 0x000114B0 File Offset: 0x0000F6B0
		internal static Exception MoreThanOneMatch()
		{
			return new InvalidOperationException("Sequence contains more than one matching element");
		}

		// Token: 0x06000190 RID: 400 RVA: 0x000114BC File Offset: 0x0000F6BC
		internal static Exception NotYetImplemented()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000191 RID: 401 RVA: 0x000114C3 File Offset: 0x0000F6C3
		internal static Exception NotYetImplemented(string message)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000192 RID: 402 RVA: 0x000114CA File Offset: 0x0000F6CA
		internal static Exception NotSupported()
		{
			return new NotSupportedException();
		}
	}
}
