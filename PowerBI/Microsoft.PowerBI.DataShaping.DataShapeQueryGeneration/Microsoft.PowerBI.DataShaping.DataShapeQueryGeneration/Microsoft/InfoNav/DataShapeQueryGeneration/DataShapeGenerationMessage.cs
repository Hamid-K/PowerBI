using System;
using System.Globalization;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Utils;
using Microsoft.PowerBI.Query.Contracts;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200004B RID: 75
	[Serializable]
	internal sealed class DataShapeGenerationMessage : EngineMessageBase
	{
		// Token: 0x0600027D RID: 637 RVA: 0x0000AB0F File Offset: 0x00008D0F
		internal DataShapeGenerationMessage(string message, string traceMessage, DataShapeGenerationErrorCode errorCode, EngineMessageSeverity severity, ErrorSource source, string[] affectedItems)
			: base(message, traceMessage, severity, source, affectedItems)
		{
			this._errorCode = errorCode;
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600027E RID: 638 RVA: 0x0000AB26 File Offset: 0x00008D26
		private static CultureInfo FormatCulture
		{
			get
			{
				return CultureInfo.CurrentCulture;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000AB2D File Offset: 0x00008D2D
		public DataShapeGenerationErrorCode ErrorCode
		{
			get
			{
				return this._errorCode;
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000AB35 File Offset: 0x00008D35
		public static DataShapeGenerationMessage Create(DataShapeGenerationErrorCode errorCode, string messageTemplate, EngineMessageSeverity severity, ErrorSource source, params object[] args)
		{
			return DataShapeGenerationMessage.Create(errorCode, messageTemplate, null, severity, source, args);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000AB44 File Offset: 0x00008D44
		public static DataShapeGenerationMessage Create(DataShapeGenerationErrorCode errorCode, string messageTemplate, IContainsTelemetryMarkup[] affectedItems, EngineMessageSeverity severity, ErrorSource source, params object[] args)
		{
			string[] array = DataShapeGenerationMessage.StringifyArguments(args);
			string[] array2 = DataShapeGenerationMessage.StringifyArguments(affectedItems);
			object obj;
			if (!args.IsNullOrEmpty<object>())
			{
				IFormatProvider formatCulture = DataShapeGenerationMessage.FormatCulture;
				object[] array3 = array;
				obj = string.Format(formatCulture, messageTemplate, array3);
			}
			else
			{
				obj = messageTemplate;
			}
			object obj2 = obj;
			return new DataShapeGenerationMessage(obj2, obj2, errorCode, severity, source, array2);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000AB88 File Offset: 0x00008D88
		private static string[] StringifyArguments(object[] arguments)
		{
			if (arguments == null)
			{
				return new string[0];
			}
			string[] array = new string[arguments.Length];
			for (int i = 0; i < arguments.Length; i++)
			{
				object obj = arguments[i];
				Identifier identifier = obj as Identifier;
				DataShapeGenerationMessagePhrase dataShapeGenerationMessagePhrase = obj as DataShapeGenerationMessagePhrase;
				if (identifier != null)
				{
					obj = DataShapeGenerationMessage.GetDisplayString(identifier);
				}
				else if (dataShapeGenerationMessagePhrase != null)
				{
					obj = dataShapeGenerationMessagePhrase.FormattedString;
				}
				IContainsTelemetryMarkup containsTelemetryMarkup = obj as IContainsTelemetryMarkup;
				if (containsTelemetryMarkup != null)
				{
					obj = containsTelemetryMarkup.ToCustomerContentString();
				}
				string text = obj as string;
				if (text != null)
				{
					array[i] = text;
				}
				else
				{
					array[i] = obj.ToString();
				}
			}
			return array;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000AC18 File Offset: 0x00008E18
		public override string GetErrorCodeString()
		{
			return this.ErrorCode.ToString();
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000AC39 File Offset: 0x00008E39
		private static string GetDisplayString(Identifier identifier)
		{
			if (!(identifier == null))
			{
				return identifier.Value;
			}
			return null;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000AC4C File Offset: 0x00008E4C
		public override bool Equals(EngineMessageBase other)
		{
			bool flag;
			DataShapeGenerationMessage dataShapeGenerationMessage;
			if (EngineMessageBase.CheckReferenceAndBaseEquality<DataShapeGenerationMessage>(this, other, out flag, out dataShapeGenerationMessage))
			{
				return flag;
			}
			return this._errorCode == dataShapeGenerationMessage._errorCode;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000AC76 File Offset: 0x00008E76
		protected override int GetDerivedTypeHashCodeContent()
		{
			return Hashing.GetHashCode<DataShapeGenerationErrorCode>(this._errorCode, null);
		}

		// Token: 0x040001FD RID: 509
		private readonly DataShapeGenerationErrorCode _errorCode;
	}
}
