using System;
using System.Reflection;
using System.Transactions;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000F9 RID: 249
	internal static class SysTxForGlobalTransactions
	{
		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x0600145D RID: 5213 RVA: 0x0004FE98 File Offset: 0x0004E098
		public static MethodInfo EnlistPromotableSinglePhase
		{
			get
			{
				return SysTxForGlobalTransactions._enlistPromotableSinglePhase.Value;
			}
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x0600145E RID: 5214 RVA: 0x0004FEA4 File Offset: 0x0004E0A4
		public static MethodInfo SetDistributedTransactionIdentifier
		{
			get
			{
				return SysTxForGlobalTransactions._setDistributedTransactionIdentifier.Value;
			}
		}

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x0600145F RID: 5215 RVA: 0x0004FEB0 File Offset: 0x0004E0B0
		public static MethodInfo GetPromotedToken
		{
			get
			{
				return SysTxForGlobalTransactions._getPromotedToken.Value;
			}
		}

		// Token: 0x040007E9 RID: 2025
		private static readonly Lazy<MethodInfo> _enlistPromotableSinglePhase = new Lazy<MethodInfo>(() => typeof(Transaction).GetMethod("EnlistPromotableSinglePhase", new Type[]
		{
			typeof(IPromotableSinglePhaseNotification),
			typeof(Guid)
		}));

		// Token: 0x040007EA RID: 2026
		private static readonly Lazy<MethodInfo> _setDistributedTransactionIdentifier = new Lazy<MethodInfo>(() => typeof(Transaction).GetMethod("SetDistributedTransactionIdentifier", new Type[]
		{
			typeof(IPromotableSinglePhaseNotification),
			typeof(Guid)
		}));

		// Token: 0x040007EB RID: 2027
		private static readonly Lazy<MethodInfo> _getPromotedToken = new Lazy<MethodInfo>(() => typeof(Transaction).GetMethod("GetPromotedToken"));
	}
}
