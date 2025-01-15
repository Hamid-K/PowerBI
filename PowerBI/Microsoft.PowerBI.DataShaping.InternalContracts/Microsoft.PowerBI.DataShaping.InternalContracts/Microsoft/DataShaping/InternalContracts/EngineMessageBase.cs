using System;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.PowerBI.Query.Contracts;

namespace Microsoft.DataShaping.InternalContracts
{
	// Token: 0x02000018 RID: 24
	[Serializable]
	internal abstract class EngineMessageBase : IEquatable<EngineMessageBase>
	{
		// Token: 0x06000045 RID: 69 RVA: 0x00002D4B File Offset: 0x00000F4B
		protected EngineMessageBase(string message, string traceMessage, EngineMessageSeverity severity, ErrorSource source = ErrorSource.Unknown, string[] affectedItems = null)
		{
			this.Message = message;
			this.Severity = severity;
			this.Source = source;
			this.TraceMessage = traceMessage;
			this.AffectedItems = affectedItems;
			this.StackTrace = "Stack trace is only available in debug builds.";
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002D83 File Offset: 0x00000F83
		public bool IsError
		{
			get
			{
				return this.Severity == EngineMessageSeverity.Error;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002D8E File Offset: 0x00000F8E
		public string Message { get; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002D96 File Offset: 0x00000F96
		public string TraceMessage { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002D9E File Offset: 0x00000F9E
		public EngineMessageSeverity Severity { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002DA6 File Offset: 0x00000FA6
		public ErrorSource Source { get; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002DAE File Offset: 0x00000FAE
		public string[] AffectedItems { get; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002DB6 File Offset: 0x00000FB6
		public string StackTrace { get; }

		// Token: 0x0600004D RID: 77
		public abstract string GetErrorCodeString();

		// Token: 0x0600004E RID: 78 RVA: 0x00002DBE File Offset: 0x00000FBE
		public override bool Equals(object obj)
		{
			return this.Equals(obj as EngineMessageBase);
		}

		// Token: 0x0600004F RID: 79
		public abstract bool Equals(EngineMessageBase other);

		// Token: 0x06000050 RID: 80 RVA: 0x00002DCC File Offset: 0x00000FCC
		protected static bool CheckReferenceAndBaseEquality<TMessage>(TMessage @this, EngineMessageBase other, out bool areEqual, out TMessage otherTyped) where TMessage : EngineMessageBase
		{
			if (CompareUtil.CheckReferenceAndTypeEquality<TMessage, EngineMessageBase>(@this, other, out areEqual, out otherTyped))
			{
				return true;
			}
			areEqual = @this.Message == otherTyped.Message && @this.Severity == otherTyped.Severity && @this.Source == otherTyped.Source && @this.TraceMessage == otherTyped.TraceMessage && @this.AffectedItems.SequenceEqualReadOnly(otherTyped.AffectedItems, StringComparer.Ordinal);
			return !areEqual;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002E80 File Offset: 0x00001080
		public override int GetHashCode()
		{
			return Hashing.CombineHash(Hashing.GetHashCode<string>(this.Message, null), Hashing.GetHashCode<EngineMessageSeverity>(this.Severity, null), Hashing.GetHashCode<ErrorSource>(this.Source, null), Hashing.GetHashCode<string>(this.TraceMessage, null), (this.AffectedItems == null) ? (-48879) : Hashing.CombineHash<string>(this.AffectedItems, StringComparer.Ordinal), this.GetDerivedTypeHashCodeContent());
		}

		// Token: 0x06000052 RID: 82
		protected abstract int GetDerivedTypeHashCodeContent();
	}
}
