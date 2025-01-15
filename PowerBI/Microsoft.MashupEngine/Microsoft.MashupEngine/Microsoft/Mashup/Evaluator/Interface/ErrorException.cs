using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001DCC RID: 7628
	[Serializable]
	public class ErrorException : Exception
	{
		// Token: 0x0600BCF0 RID: 48368 RVA: 0x0026597F File Offset: 0x00263B7F
		public ErrorException(string message, ErrorException innerException)
			: this(message, null, null, innerException.Details, innerException.IsRecoverable, innerException.IsExpected, innerException)
		{
		}

		// Token: 0x0600BCF1 RID: 48369 RVA: 0x0026599D File Offset: 0x00263B9D
		public ErrorException(string message, bool isRecoverable, bool isExpected)
			: this(message, null, null, isRecoverable, isExpected, null)
		{
		}

		// Token: 0x0600BCF2 RID: 48370 RVA: 0x002659AB File Offset: 0x00263BAB
		public ErrorException(string message, string stackTrace, string className, bool isRecoverable, bool isExpected)
			: this(message, stackTrace, className, isRecoverable, isExpected, null)
		{
		}

		// Token: 0x0600BCF3 RID: 48371 RVA: 0x002659BB File Offset: 0x00263BBB
		public ErrorException(string message, string stackTrace, string className, bool isRecoverable, bool isExpected, ErrorException innerException)
			: this(message, stackTrace, className, null, isRecoverable, isExpected, innerException)
		{
		}

		// Token: 0x0600BCF4 RID: 48372 RVA: 0x002659CD File Offset: 0x00263BCD
		public ErrorException(string message, string stackTrace, string className, string details, bool isRecoverable, bool isExpected, ErrorException innerException)
			: base(message, innerException)
		{
			this.className = className;
			this.details = details;
			this.isRecoverable = isRecoverable;
			this.isExpected = isExpected;
			if (stackTrace != null)
			{
				ExceptionInternals.TrySetStackTraceString(this, stackTrace);
			}
			if (className != null)
			{
				ExceptionInternals.TrySetClassName(this, className);
			}
		}

		// Token: 0x0600BCF5 RID: 48373 RVA: 0x00265A10 File Offset: 0x00263C10
		protected ErrorException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.isRecoverable = (bool)info.GetValue("isRecoverable", typeof(bool));
			this.isExpected = (bool)info.GetValue("isExpected", typeof(bool));
		}

		// Token: 0x17002E79 RID: 11897
		// (get) Token: 0x0600BCF6 RID: 48374 RVA: 0x00265A65 File Offset: 0x00263C65
		public string ClassName
		{
			get
			{
				return this.className;
			}
		}

		// Token: 0x17002E7A RID: 11898
		// (get) Token: 0x0600BCF7 RID: 48375 RVA: 0x00265A6D File Offset: 0x00263C6D
		public string Details
		{
			get
			{
				return this.details;
			}
		}

		// Token: 0x17002E7B RID: 11899
		// (get) Token: 0x0600BCF8 RID: 48376 RVA: 0x00265A75 File Offset: 0x00263C75
		public bool IsRecoverable
		{
			get
			{
				return this.isRecoverable;
			}
		}

		// Token: 0x17002E7C RID: 11900
		// (get) Token: 0x0600BCF9 RID: 48377 RVA: 0x00265A7D File Offset: 0x00263C7D
		public bool IsExpected
		{
			get
			{
				return this.isExpected;
			}
		}

		// Token: 0x17002E7D RID: 11901
		// (get) Token: 0x0600BCFA RID: 48378 RVA: 0x00265A85 File Offset: 0x00263C85
		public new ErrorException InnerException
		{
			get
			{
				return (ErrorException)base.InnerException;
			}
		}

		// Token: 0x0600BCFB RID: 48379 RVA: 0x00265A94 File Offset: 0x00263C94
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("isRecoverable", this.isRecoverable, typeof(bool));
			info.AddValue("isExpected", this.isExpected, typeof(bool));
			base.GetObjectData(info, context);
		}

		// Token: 0x0600BCFC RID: 48380 RVA: 0x00265AEC File Offset: 0x00263CEC
		public override string ToString()
		{
			string text = base.ToString();
			if (this.details == null)
			{
				return text;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(text);
			stringBuilder.AppendLine("Details:");
			stringBuilder.AppendLine(this.details);
			return stringBuilder.ToString();
		}

		// Token: 0x04006065 RID: 24677
		private const string isRecoverableName = "isRecoverable";

		// Token: 0x04006066 RID: 24678
		private const string isExpectedName = "isExpected";

		// Token: 0x04006067 RID: 24679
		private readonly string className;

		// Token: 0x04006068 RID: 24680
		private readonly string details;

		// Token: 0x04006069 RID: 24681
		private readonly bool isRecoverable;

		// Token: 0x0400606A RID: 24682
		private readonly bool isExpected;
	}
}
