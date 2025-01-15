using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.MQ
{
	// Token: 0x0200093D RID: 2365
	public sealed class CicsBridgeHeader : MqHeader
	{
		// Token: 0x06004357 RID: 17239 RVA: 0x000E3AAB File Offset: 0x000E1CAB
		public CicsBridgeHeader()
		{
			base.RealValue = CicsBridgeHeader.Constructor.Invoke(null);
		}

		// Token: 0x06004358 RID: 17240 RVA: 0x000E3AC4 File Offset: 0x000E1CC4
		public CicsBridgeHeader(object header)
			: base(header)
		{
		}

		// Token: 0x17001563 RID: 5475
		// (get) Token: 0x06004359 RID: 17241 RVA: 0x000E3ACD File Offset: 0x000E1CCD
		// (set) Token: 0x0600435A RID: 17242 RVA: 0x000E3AD4 File Offset: 0x000E1CD4
		public static Type RealType { get; private set; } = AssemblyLoader.HisConnectors.GetType("Microsoft.HostIntegration.MqClient.CicsBridgeHeader");

		// Token: 0x17001564 RID: 5476
		// (get) Token: 0x0600435B RID: 17243 RVA: 0x000E3ADC File Offset: 0x000E1CDC
		// (set) Token: 0x0600435C RID: 17244 RVA: 0x000E3AF4 File Offset: 0x000E1CF4
		public CicsBridgeHeader.CicsBridgeFlag Flags
		{
			get
			{
				return (CicsBridgeHeader.CicsBridgeFlag)CicsBridgeHeader.FlagsInfo.GetValue(base.RealValue, null);
			}
			set
			{
				CicsBridgeHeader.FlagsInfo.SetValue(base.RealValue, value, null);
			}
		}

		// Token: 0x17001565 RID: 5477
		// (get) Token: 0x0600435D RID: 17245 RVA: 0x000E3B10 File Offset: 0x000E1D10
		public Value FlagsString
		{
			get
			{
				CicsBridgeHeader.CicsBridgeFlag cicsBridgeFlag = (CicsBridgeHeader.CicsBridgeFlag)CicsBridgeHeader.FlagsInfo.GetValue(base.RealValue, null);
				string text;
				if (CicsBridgeHeader.cicsBridgeFlagStrings.TryGetValue(cicsBridgeFlag, out text))
				{
					return TextValue.New(text);
				}
				return Value.Null;
			}
		}

		// Token: 0x17001566 RID: 5478
		// (get) Token: 0x0600435E RID: 17246 RVA: 0x000E3B4F File Offset: 0x000E1D4F
		// (set) Token: 0x0600435F RID: 17247 RVA: 0x000E3B67 File Offset: 0x000E1D67
		public CicsBridgeHeader.CicsBridgeReturnCode ReturnCode
		{
			get
			{
				return (CicsBridgeHeader.CicsBridgeReturnCode)CicsBridgeHeader.ReturnCodeInfo.GetValue(base.RealValue, null);
			}
			set
			{
				CicsBridgeHeader.ReturnCodeInfo.SetValue(base.RealValue, value, null);
			}
		}

		// Token: 0x17001567 RID: 5479
		// (get) Token: 0x06004360 RID: 17248 RVA: 0x000E3B80 File Offset: 0x000E1D80
		public Value ReturnCodeString
		{
			get
			{
				CicsBridgeHeader.CicsBridgeReturnCode cicsBridgeReturnCode = (CicsBridgeHeader.CicsBridgeReturnCode)CicsBridgeHeader.ReturnCodeInfo.GetValue(base.RealValue, null);
				string text;
				if (CicsBridgeHeader.cicsBridgeReturnCodeStrings.TryGetValue(cicsBridgeReturnCode, out text))
				{
					return TextValue.New(text);
				}
				return Value.Null;
			}
		}

		// Token: 0x17001568 RID: 5480
		// (get) Token: 0x06004361 RID: 17249 RVA: 0x000E3BBF File Offset: 0x000E1DBF
		// (set) Token: 0x06004362 RID: 17250 RVA: 0x000E3BD7 File Offset: 0x000E1DD7
		public int CompletionCode
		{
			get
			{
				return (int)CicsBridgeHeader.CompletionCodeInfo.GetValue(base.RealValue, null);
			}
			set
			{
				CicsBridgeHeader.CompletionCodeInfo.SetValue(base.RealValue, value, null);
			}
		}

		// Token: 0x17001569 RID: 5481
		// (get) Token: 0x06004363 RID: 17251 RVA: 0x000E3BF0 File Offset: 0x000E1DF0
		// (set) Token: 0x06004364 RID: 17252 RVA: 0x000E3C08 File Offset: 0x000E1E08
		public int ReasonCode
		{
			get
			{
				return (int)CicsBridgeHeader.ReasonCodeInfo.GetValue(base.RealValue, null);
			}
			set
			{
				CicsBridgeHeader.ReasonCodeInfo.SetValue(base.RealValue, value, null);
			}
		}

		// Token: 0x1700156A RID: 5482
		// (get) Token: 0x06004365 RID: 17253 RVA: 0x000E3C21 File Offset: 0x000E1E21
		// (set) Token: 0x06004366 RID: 17254 RVA: 0x000E3C39 File Offset: 0x000E1E39
		public CicsBridgeHeader.CicsBridgeUowControl UowControl
		{
			get
			{
				return (CicsBridgeHeader.CicsBridgeUowControl)CicsBridgeHeader.UowControlInfo.GetValue(base.RealValue, null);
			}
			set
			{
				CicsBridgeHeader.UowControlInfo.SetValue(base.RealValue, value, null);
			}
		}

		// Token: 0x1700156B RID: 5483
		// (get) Token: 0x06004367 RID: 17255 RVA: 0x000E3C54 File Offset: 0x000E1E54
		public Value UowControlString
		{
			get
			{
				CicsBridgeHeader.CicsBridgeUowControl cicsBridgeUowControl = (CicsBridgeHeader.CicsBridgeUowControl)CicsBridgeHeader.UowControlInfo.GetValue(base.RealValue, null);
				string text;
				if (CicsBridgeHeader.cicsBridgeUowControlStrings.TryGetValue(cicsBridgeUowControl, out text))
				{
					return TextValue.New(text);
				}
				return Value.Null;
			}
		}

		// Token: 0x1700156C RID: 5484
		// (get) Token: 0x06004368 RID: 17256 RVA: 0x000E3C93 File Offset: 0x000E1E93
		// (set) Token: 0x06004369 RID: 17257 RVA: 0x000E3CAB File Offset: 0x000E1EAB
		public int GetWaitInterval
		{
			get
			{
				return (int)CicsBridgeHeader.GetWaitIntervalInfo.GetValue(base.RealValue, null);
			}
			set
			{
				CicsBridgeHeader.GetWaitIntervalInfo.SetValue(base.RealValue, value, null);
			}
		}

		// Token: 0x1700156D RID: 5485
		// (get) Token: 0x0600436A RID: 17258 RVA: 0x000E3CC4 File Offset: 0x000E1EC4
		// (set) Token: 0x0600436B RID: 17259 RVA: 0x000E3CDC File Offset: 0x000E1EDC
		public CicsBridgeHeader.CicsBridgeLinkType LinkType
		{
			get
			{
				return (CicsBridgeHeader.CicsBridgeLinkType)CicsBridgeHeader.LinkTypeInfo.GetValue(base.RealValue, null);
			}
			set
			{
				CicsBridgeHeader.LinkTypeInfo.SetValue(base.RealValue, value, null);
			}
		}

		// Token: 0x1700156E RID: 5486
		// (get) Token: 0x0600436C RID: 17260 RVA: 0x000E3CF8 File Offset: 0x000E1EF8
		public Value LinkTypeString
		{
			get
			{
				CicsBridgeHeader.CicsBridgeLinkType cicsBridgeLinkType = (CicsBridgeHeader.CicsBridgeLinkType)CicsBridgeHeader.LinkTypeInfo.GetValue(base.RealValue, null);
				string text;
				if (CicsBridgeHeader.cicsBridgeLinkTypeStrings.TryGetValue(cicsBridgeLinkType, out text))
				{
					return TextValue.New(text);
				}
				return Value.Null;
			}
		}

		// Token: 0x1700156F RID: 5487
		// (get) Token: 0x0600436D RID: 17261 RVA: 0x000E3D37 File Offset: 0x000E1F37
		// (set) Token: 0x0600436E RID: 17262 RVA: 0x000E3D4F File Offset: 0x000E1F4F
		public int OutputDataLength
		{
			get
			{
				return (int)CicsBridgeHeader.OutputDataLengthInfo.GetValue(base.RealValue, null);
			}
			set
			{
				CicsBridgeHeader.OutputDataLengthInfo.SetValue(base.RealValue, value, null);
			}
		}

		// Token: 0x17001570 RID: 5488
		// (get) Token: 0x0600436F RID: 17263 RVA: 0x000E3D68 File Offset: 0x000E1F68
		// (set) Token: 0x06004370 RID: 17264 RVA: 0x000E3D80 File Offset: 0x000E1F80
		public int FacilityKeepTime
		{
			get
			{
				return (int)CicsBridgeHeader.FacilityKeepTimeInfo.GetValue(base.RealValue, null);
			}
			set
			{
				CicsBridgeHeader.FacilityKeepTimeInfo.SetValue(base.RealValue, value, null);
			}
		}

		// Token: 0x17001571 RID: 5489
		// (get) Token: 0x06004371 RID: 17265 RVA: 0x000E3D99 File Offset: 0x000E1F99
		// (set) Token: 0x06004372 RID: 17266 RVA: 0x000E3DB1 File Offset: 0x000E1FB1
		public CicsBridgeHeader.CicsBridgeAdsDescriptor AdsDescriptors
		{
			get
			{
				return (CicsBridgeHeader.CicsBridgeAdsDescriptor)CicsBridgeHeader.AdsDescriptorsInfo.GetValue(base.RealValue, null);
			}
			set
			{
				CicsBridgeHeader.AdsDescriptorsInfo.SetValue(base.RealValue, value, null);
			}
		}

		// Token: 0x17001572 RID: 5490
		// (get) Token: 0x06004373 RID: 17267 RVA: 0x000E3DCC File Offset: 0x000E1FCC
		public Value AdsDescriptorsString
		{
			get
			{
				CicsBridgeHeader.CicsBridgeAdsDescriptor cicsBridgeAdsDescriptor = (CicsBridgeHeader.CicsBridgeAdsDescriptor)CicsBridgeHeader.AdsDescriptorsInfo.GetValue(base.RealValue, null);
				string text;
				if (CicsBridgeHeader.cicsBridgeAdsDescriptorStrings.TryGetValue(cicsBridgeAdsDescriptor, out text))
				{
					return TextValue.New(text);
				}
				return Value.Null;
			}
		}

		// Token: 0x17001573 RID: 5491
		// (get) Token: 0x06004374 RID: 17268 RVA: 0x000E3E0B File Offset: 0x000E200B
		// (set) Token: 0x06004375 RID: 17269 RVA: 0x000E3E23 File Offset: 0x000E2023
		public bool IsConversationalTask
		{
			get
			{
				return (bool)CicsBridgeHeader.IsConversationalTaskInfo.GetValue(base.RealValue, null);
			}
			set
			{
				CicsBridgeHeader.IsConversationalTaskInfo.SetValue(base.RealValue, value, null);
			}
		}

		// Token: 0x17001574 RID: 5492
		// (get) Token: 0x06004376 RID: 17270 RVA: 0x000E3E3C File Offset: 0x000E203C
		// (set) Token: 0x06004377 RID: 17271 RVA: 0x000E3E54 File Offset: 0x000E2054
		public CicsBridgeHeader.CicsBridgeTaskEndStatus TaskEndStatus
		{
			get
			{
				return (CicsBridgeHeader.CicsBridgeTaskEndStatus)CicsBridgeHeader.TaskEndStatusInfo.GetValue(base.RealValue, null);
			}
			set
			{
				CicsBridgeHeader.TaskEndStatusInfo.SetValue(base.RealValue, value, null);
			}
		}

		// Token: 0x17001575 RID: 5493
		// (get) Token: 0x06004378 RID: 17272 RVA: 0x000E3E70 File Offset: 0x000E2070
		public Value TaskEndStatusString
		{
			get
			{
				CicsBridgeHeader.CicsBridgeTaskEndStatus cicsBridgeTaskEndStatus = (CicsBridgeHeader.CicsBridgeTaskEndStatus)CicsBridgeHeader.TaskEndStatusInfo.GetValue(base.RealValue, null);
				string text;
				if (CicsBridgeHeader.cicsBridgeTaskEndStatusStrings.TryGetValue(cicsBridgeTaskEndStatus, out text))
				{
					return TextValue.New(text);
				}
				return Value.Null;
			}
		}

		// Token: 0x17001576 RID: 5494
		// (get) Token: 0x06004379 RID: 17273 RVA: 0x000E3EAF File Offset: 0x000E20AF
		// (set) Token: 0x0600437A RID: 17274 RVA: 0x000E3EC7 File Offset: 0x000E20C7
		public byte[] Token
		{
			get
			{
				return (byte[])CicsBridgeHeader.TokenInfo.GetValue(base.RealValue, null);
			}
			set
			{
				CicsBridgeHeader.TokenInfo.SetValue(base.RealValue, value, null);
			}
		}

		// Token: 0x17001577 RID: 5495
		// (get) Token: 0x0600437B RID: 17275 RVA: 0x000E3EDB File Offset: 0x000E20DB
		// (set) Token: 0x0600437C RID: 17276 RVA: 0x000E3EF3 File Offset: 0x000E20F3
		public string Function
		{
			get
			{
				return (string)CicsBridgeHeader.FunctionInfo.GetValue(base.RealValue, null);
			}
			set
			{
				CicsBridgeHeader.FunctionInfo.SetValue(base.RealValue, value, null);
			}
		}

		// Token: 0x17001578 RID: 5496
		// (get) Token: 0x0600437D RID: 17277 RVA: 0x000E3F07 File Offset: 0x000E2107
		// (set) Token: 0x0600437E RID: 17278 RVA: 0x000E3F1F File Offset: 0x000E211F
		public string AbendCode
		{
			get
			{
				return (string)CicsBridgeHeader.AbendCodeInfo.GetValue(base.RealValue, null);
			}
			set
			{
				CicsBridgeHeader.AbendCodeInfo.SetValue(base.RealValue, value, null);
			}
		}

		// Token: 0x17001579 RID: 5497
		// (get) Token: 0x0600437F RID: 17279 RVA: 0x000E3F33 File Offset: 0x000E2133
		// (set) Token: 0x06004380 RID: 17280 RVA: 0x000E3F4B File Offset: 0x000E214B
		public string Authenticator
		{
			get
			{
				return (string)CicsBridgeHeader.AuthenticatorInfo.GetValue(base.RealValue, null);
			}
			set
			{
				CicsBridgeHeader.AuthenticatorInfo.SetValue(base.RealValue, value, null);
			}
		}

		// Token: 0x1700157A RID: 5498
		// (get) Token: 0x06004381 RID: 17281 RVA: 0x000E3F5F File Offset: 0x000E215F
		// (set) Token: 0x06004382 RID: 17282 RVA: 0x000E3F77 File Offset: 0x000E2177
		public string ReplyToFormat
		{
			get
			{
				return (string)CicsBridgeHeader.ReplyToFormatInfo.GetValue(base.RealValue, null);
			}
			set
			{
				CicsBridgeHeader.ReplyToFormatInfo.SetValue(base.RealValue, value, null);
			}
		}

		// Token: 0x1700157B RID: 5499
		// (get) Token: 0x06004383 RID: 17283 RVA: 0x000E3F8B File Offset: 0x000E218B
		// (set) Token: 0x06004384 RID: 17284 RVA: 0x000E3FA3 File Offset: 0x000E21A3
		public string RemoteSysId
		{
			get
			{
				return (string)CicsBridgeHeader.RemoteSysIdInfo.GetValue(base.RealValue, null);
			}
			set
			{
				CicsBridgeHeader.RemoteSysIdInfo.SetValue(base.RealValue, value, null);
			}
		}

		// Token: 0x1700157C RID: 5500
		// (get) Token: 0x06004385 RID: 17285 RVA: 0x000E3FB7 File Offset: 0x000E21B7
		// (set) Token: 0x06004386 RID: 17286 RVA: 0x000E3FCF File Offset: 0x000E21CF
		public string RemoteTransactionId
		{
			get
			{
				return (string)CicsBridgeHeader.RemoteTransactionIdInfo.GetValue(base.RealValue, null);
			}
			set
			{
				CicsBridgeHeader.RemoteTransactionIdInfo.SetValue(base.RealValue, value, null);
			}
		}

		// Token: 0x1700157D RID: 5501
		// (get) Token: 0x06004387 RID: 17287 RVA: 0x000E3FE3 File Offset: 0x000E21E3
		// (set) Token: 0x06004388 RID: 17288 RVA: 0x000E3FFB File Offset: 0x000E21FB
		public string TransactionId
		{
			get
			{
				return (string)CicsBridgeHeader.TransactionIdInfo.GetValue(base.RealValue, null);
			}
			set
			{
				CicsBridgeHeader.TransactionIdInfo.SetValue(base.RealValue, value, null);
			}
		}

		// Token: 0x1700157E RID: 5502
		// (get) Token: 0x06004389 RID: 17289 RVA: 0x000E400F File Offset: 0x000E220F
		// (set) Token: 0x0600438A RID: 17290 RVA: 0x000E4027 File Offset: 0x000E2227
		public string FacilityIsLike
		{
			get
			{
				return (string)CicsBridgeHeader.FacilityIsLikeInfo.GetValue(base.RealValue, null);
			}
			set
			{
				CicsBridgeHeader.FacilityIsLikeInfo.SetValue(base.RealValue, value, null);
			}
		}

		// Token: 0x1700157F RID: 5503
		// (get) Token: 0x0600438B RID: 17291 RVA: 0x000E403B File Offset: 0x000E223B
		// (set) Token: 0x0600438C RID: 17292 RVA: 0x000E4053 File Offset: 0x000E2253
		public byte AttentionId
		{
			get
			{
				return (byte)CicsBridgeHeader.AttentionIdInfo.GetValue(base.RealValue, null);
			}
			set
			{
				CicsBridgeHeader.AttentionIdInfo.SetValue(base.RealValue, value, null);
			}
		}

		// Token: 0x17001580 RID: 5504
		// (get) Token: 0x0600438D RID: 17293 RVA: 0x000E406C File Offset: 0x000E226C
		// (set) Token: 0x0600438E RID: 17294 RVA: 0x000E4084 File Offset: 0x000E2284
		public CicsBridgeHeader.CicsBridgeStartCode StartCode
		{
			get
			{
				return (CicsBridgeHeader.CicsBridgeStartCode)CicsBridgeHeader.StartCodeInfo.GetValue(base.RealValue, null);
			}
			set
			{
				CicsBridgeHeader.StartCodeInfo.SetValue(base.RealValue, value, null);
			}
		}

		// Token: 0x17001581 RID: 5505
		// (get) Token: 0x0600438F RID: 17295 RVA: 0x000E40A0 File Offset: 0x000E22A0
		public Value StartCodeString
		{
			get
			{
				CicsBridgeHeader.CicsBridgeStartCode cicsBridgeStartCode = (CicsBridgeHeader.CicsBridgeStartCode)CicsBridgeHeader.StartCodeInfo.GetValue(base.RealValue, null);
				string text;
				if (CicsBridgeHeader.cicsBridgeStartCodeStrings.TryGetValue(cicsBridgeStartCode, out text))
				{
					return TextValue.New(text);
				}
				return Value.Null;
			}
		}

		// Token: 0x17001582 RID: 5506
		// (get) Token: 0x06004390 RID: 17296 RVA: 0x000E40DF File Offset: 0x000E22DF
		// (set) Token: 0x06004391 RID: 17297 RVA: 0x000E40F7 File Offset: 0x000E22F7
		public string CancelCode
		{
			get
			{
				return (string)CicsBridgeHeader.CancelCodeInfo.GetValue(base.RealValue, null);
			}
			set
			{
				CicsBridgeHeader.CancelCodeInfo.SetValue(base.RealValue, value, null);
			}
		}

		// Token: 0x17001583 RID: 5507
		// (get) Token: 0x06004392 RID: 17298 RVA: 0x000E410B File Offset: 0x000E230B
		// (set) Token: 0x06004393 RID: 17299 RVA: 0x000E4123 File Offset: 0x000E2323
		public string NextTransactionId
		{
			get
			{
				return (string)CicsBridgeHeader.NextTransactionIdInfo.GetValue(base.RealValue, null);
			}
			set
			{
				CicsBridgeHeader.NextTransactionIdInfo.SetValue(base.RealValue, value, null);
			}
		}

		// Token: 0x17001584 RID: 5508
		// (get) Token: 0x06004394 RID: 17300 RVA: 0x000E4137 File Offset: 0x000E2337
		// (set) Token: 0x06004395 RID: 17301 RVA: 0x000E414F File Offset: 0x000E234F
		public int CursorPosition
		{
			get
			{
				return (int)CicsBridgeHeader.CursorPositionInfo.GetValue(base.RealValue, null);
			}
			set
			{
				CicsBridgeHeader.CursorPositionInfo.SetValue(base.RealValue, value, null);
			}
		}

		// Token: 0x17001585 RID: 5509
		// (get) Token: 0x06004396 RID: 17302 RVA: 0x000E4168 File Offset: 0x000E2368
		// (set) Token: 0x06004397 RID: 17303 RVA: 0x000E4180 File Offset: 0x000E2380
		public int ErrorOffset
		{
			get
			{
				return (int)CicsBridgeHeader.ErrorOffsetInfo.GetValue(base.RealValue, null);
			}
			set
			{
				CicsBridgeHeader.ErrorOffsetInfo.SetValue(base.RealValue, value, null);
			}
		}

		// Token: 0x06004398 RID: 17304 RVA: 0x000E419C File Offset: 0x000E239C
		public override Value GetRecordValue(Value binaryDisplay, TypeValue binaryDisplayTypeValue)
		{
			return RecordValue.New(CicsBridgeHeader.RecordKeys, new Value[]
			{
				TextValue.NewOrNull(this.AbendCode),
				this.AdsDescriptorsString,
				NumberValue.New((int)this.AttentionId),
				TextValue.NewOrNull(this.Authenticator),
				TextValue.NewOrNull(this.CancelCode),
				NumberValue.New(this.CompletionCode),
				NumberValue.New(this.CursorPosition),
				NumberValue.New(this.ErrorOffset),
				LogicalValue.New(this.IsConversationalTask),
				NumberValue.New(this.FacilityKeepTime),
				TextValue.NewOrNull(this.FacilityIsLike),
				this.FlagsString,
				TextValue.NewOrNull(base.FormatString),
				TextValue.NewOrNull(this.Function),
				NumberValue.New(this.GetWaitInterval),
				this.LinkTypeString,
				TextValue.NewOrNull(this.NextTransactionId),
				NumberValue.New(this.OutputDataLength),
				NumberValue.New(this.ReasonCode),
				TextValue.NewOrNull(this.RemoteSysId),
				TextValue.NewOrNull(this.RemoteTransactionId),
				TextValue.NewOrNull(this.ReplyToFormat),
				this.ReturnCodeString,
				this.StartCodeString,
				this.TaskEndStatusString,
				Utilities.ValueFromBytes(this.Token, binaryDisplay, binaryDisplayTypeValue, 0),
				TextValue.NewOrNull(this.TransactionId),
				this.UowControlString
			});
		}

		// Token: 0x04002357 RID: 9047
		private static readonly ConstructorInfo Constructor = CicsBridgeHeader.RealType.GetConstructor(Type.EmptyTypes);

		// Token: 0x04002358 RID: 9048
		private static readonly PropertyInfo FlagsInfo = CicsBridgeHeader.RealType.GetProperty("Flags");

		// Token: 0x04002359 RID: 9049
		private static readonly PropertyInfo ReturnCodeInfo = CicsBridgeHeader.RealType.GetProperty("ReturnCode");

		// Token: 0x0400235A RID: 9050
		private static readonly PropertyInfo CompletionCodeInfo = CicsBridgeHeader.RealType.GetProperty("CompletionCode");

		// Token: 0x0400235B RID: 9051
		private static readonly PropertyInfo ReasonCodeInfo = CicsBridgeHeader.RealType.GetProperty("ReasonCode");

		// Token: 0x0400235C RID: 9052
		private static readonly PropertyInfo UowControlInfo = CicsBridgeHeader.RealType.GetProperty("UowControl");

		// Token: 0x0400235D RID: 9053
		private static readonly PropertyInfo GetWaitIntervalInfo = CicsBridgeHeader.RealType.GetProperty("GetWaitInterval");

		// Token: 0x0400235E RID: 9054
		private static readonly PropertyInfo LinkTypeInfo = CicsBridgeHeader.RealType.GetProperty("LinkType");

		// Token: 0x0400235F RID: 9055
		private static readonly PropertyInfo OutputDataLengthInfo = CicsBridgeHeader.RealType.GetProperty("OutputDataLength");

		// Token: 0x04002360 RID: 9056
		private static readonly PropertyInfo FacilityKeepTimeInfo = CicsBridgeHeader.RealType.GetProperty("FacilityKeepTime");

		// Token: 0x04002361 RID: 9057
		private static readonly PropertyInfo AdsDescriptorsInfo = CicsBridgeHeader.RealType.GetProperty("AdsDescriptors");

		// Token: 0x04002362 RID: 9058
		private static readonly PropertyInfo IsConversationalTaskInfo = CicsBridgeHeader.RealType.GetProperty("IsConversationalTask");

		// Token: 0x04002363 RID: 9059
		private static readonly PropertyInfo TaskEndStatusInfo = CicsBridgeHeader.RealType.GetProperty("TaskEndStatus");

		// Token: 0x04002364 RID: 9060
		private static readonly PropertyInfo TokenInfo = CicsBridgeHeader.RealType.GetProperty("Token");

		// Token: 0x04002365 RID: 9061
		private static readonly PropertyInfo FunctionInfo = CicsBridgeHeader.RealType.GetProperty("Function");

		// Token: 0x04002366 RID: 9062
		private static readonly PropertyInfo AbendCodeInfo = CicsBridgeHeader.RealType.GetProperty("AbendCode");

		// Token: 0x04002367 RID: 9063
		private static readonly PropertyInfo AuthenticatorInfo = CicsBridgeHeader.RealType.GetProperty("Authenticator");

		// Token: 0x04002368 RID: 9064
		private static readonly PropertyInfo ReplyToFormatInfo = CicsBridgeHeader.RealType.GetProperty("ReplyToFormat");

		// Token: 0x04002369 RID: 9065
		private static readonly PropertyInfo RemoteSysIdInfo = CicsBridgeHeader.RealType.GetProperty("RemoteSysId");

		// Token: 0x0400236A RID: 9066
		private static readonly PropertyInfo RemoteTransactionIdInfo = CicsBridgeHeader.RealType.GetProperty("RemoteTransactionId");

		// Token: 0x0400236B RID: 9067
		private static readonly PropertyInfo TransactionIdInfo = CicsBridgeHeader.RealType.GetProperty("TransactionId");

		// Token: 0x0400236C RID: 9068
		private static readonly PropertyInfo FacilityIsLikeInfo = CicsBridgeHeader.RealType.GetProperty("FacilityIsLike");

		// Token: 0x0400236D RID: 9069
		private static readonly PropertyInfo AttentionIdInfo = CicsBridgeHeader.RealType.GetProperty("AttentionId");

		// Token: 0x0400236E RID: 9070
		private static readonly PropertyInfo StartCodeInfo = CicsBridgeHeader.RealType.GetProperty("StartCode");

		// Token: 0x0400236F RID: 9071
		private static readonly PropertyInfo CancelCodeInfo = CicsBridgeHeader.RealType.GetProperty("CancelCode");

		// Token: 0x04002370 RID: 9072
		private static readonly PropertyInfo NextTransactionIdInfo = CicsBridgeHeader.RealType.GetProperty("NextTransactionId");

		// Token: 0x04002371 RID: 9073
		private static readonly PropertyInfo CursorPositionInfo = CicsBridgeHeader.RealType.GetProperty("CursorPosition");

		// Token: 0x04002372 RID: 9074
		private static readonly PropertyInfo ErrorOffsetInfo = CicsBridgeHeader.RealType.GetProperty("ErrorOffset");

		// Token: 0x04002373 RID: 9075
		private static readonly Keys RecordKeys = Keys.New(new string[]
		{
			"AbendCode", "ADSDescriptor", "AttentionId", "Authenticator", "CancelCode", "CompletionCode", "CursorPosition", "ErrorOffset", "ConversationalTask", "FacilityKeepTime",
			"FacilityLike", "Flags", "Format", "Function", "GetWaitInterval", "LinkType", "NextTransactionId", "OutputDataLength", "ReasonCode", "RemoteSysid",
			"RemoteTransactionId", "ReplyToFormat", "ReturnCode", "StartCode", "TaskEndStatus", "Token", "TransactionId", "UOWControl"
		});

		// Token: 0x04002375 RID: 9077
		private static readonly Dictionary<CicsBridgeHeader.CicsBridgeFlag, string> cicsBridgeFlagStrings = new Dictionary<CicsBridgeHeader.CicsBridgeFlag, string>
		{
			{
				CicsBridgeHeader.CicsBridgeFlag.None,
				"None"
			},
			{
				CicsBridgeHeader.CicsBridgeFlag.PassExpiration,
				"PassExpiration"
			},
			{
				CicsBridgeHeader.CicsBridgeFlag.TruncateNulls,
				"TruncateNulls"
			},
			{
				CicsBridgeHeader.CicsBridgeFlag.SyncpointOnReturn,
				"SyncpointOnReturn"
			}
		};

		// Token: 0x04002376 RID: 9078
		private static readonly Dictionary<CicsBridgeHeader.CicsBridgeReturnCode, string> cicsBridgeReturnCodeStrings = new Dictionary<CicsBridgeHeader.CicsBridgeReturnCode, string>
		{
			{
				CicsBridgeHeader.CicsBridgeReturnCode.Ok,
				"Ok"
			},
			{
				CicsBridgeHeader.CicsBridgeReturnCode.CicsExecError,
				"CicsExecError"
			},
			{
				CicsBridgeHeader.CicsBridgeReturnCode.ApiError,
				"ApiError"
			},
			{
				CicsBridgeHeader.CicsBridgeReturnCode.BridgeError,
				"BridgeError"
			},
			{
				CicsBridgeHeader.CicsBridgeReturnCode.BridgeAbend,
				"BridgeAbend"
			},
			{
				CicsBridgeHeader.CicsBridgeReturnCode.ApplicationAbend,
				"ApplicationAbend"
			},
			{
				CicsBridgeHeader.CicsBridgeReturnCode.SecurityError,
				"SecurityError"
			},
			{
				CicsBridgeHeader.CicsBridgeReturnCode.ProgramNotAvailable,
				"ProgramNotAvailable"
			},
			{
				CicsBridgeHeader.CicsBridgeReturnCode.BridgeTimeout,
				"BridgeTimeout"
			},
			{
				CicsBridgeHeader.CicsBridgeReturnCode.TransactionNotAvailable,
				"TransactionNotAvailable"
			}
		};

		// Token: 0x04002377 RID: 9079
		private static readonly Dictionary<CicsBridgeHeader.CicsBridgeUowControl, string> cicsBridgeUowControlStrings = new Dictionary<CicsBridgeHeader.CicsBridgeUowControl, string>
		{
			{
				CicsBridgeHeader.CicsBridgeUowControl.Middle,
				"Middle"
			},
			{
				CicsBridgeHeader.CicsBridgeUowControl.First,
				"First"
			},
			{
				CicsBridgeHeader.CicsBridgeUowControl.Commit,
				"Commit"
			},
			{
				CicsBridgeHeader.CicsBridgeUowControl.Last,
				"Last"
			},
			{
				CicsBridgeHeader.CicsBridgeUowControl.Only,
				"Only"
			},
			{
				CicsBridgeHeader.CicsBridgeUowControl.Backout,
				"Backout"
			},
			{
				CicsBridgeHeader.CicsBridgeUowControl.Continue,
				"Continue"
			}
		};

		// Token: 0x04002378 RID: 9080
		private static readonly Dictionary<CicsBridgeHeader.CicsBridgeLinkType, string> cicsBridgeLinkTypeStrings = new Dictionary<CicsBridgeHeader.CicsBridgeLinkType, string>
		{
			{
				CicsBridgeHeader.CicsBridgeLinkType.Program,
				"Program"
			},
			{
				CicsBridgeHeader.CicsBridgeLinkType.Transaction,
				"Transaction"
			}
		};

		// Token: 0x04002379 RID: 9081
		private static readonly Dictionary<CicsBridgeHeader.CicsBridgeAdsDescriptor, string> cicsBridgeAdsDescriptorStrings = new Dictionary<CicsBridgeHeader.CicsBridgeAdsDescriptor, string>
		{
			{
				CicsBridgeHeader.CicsBridgeAdsDescriptor.None,
				"None"
			},
			{
				CicsBridgeHeader.CicsBridgeAdsDescriptor.Send,
				"Send"
			},
			{
				CicsBridgeHeader.CicsBridgeAdsDescriptor.Receive,
				"Receive"
			},
			{
				CicsBridgeHeader.CicsBridgeAdsDescriptor.MessageFormat,
				"MessageFormat"
			}
		};

		// Token: 0x0400237A RID: 9082
		private static readonly Dictionary<CicsBridgeHeader.CicsBridgeTaskEndStatus, string> cicsBridgeTaskEndStatusStrings = new Dictionary<CicsBridgeHeader.CicsBridgeTaskEndStatus, string>
		{
			{
				CicsBridgeHeader.CicsBridgeTaskEndStatus.NoSync,
				"NoSync"
			},
			{
				CicsBridgeHeader.CicsBridgeTaskEndStatus.Commit,
				"Commit"
			},
			{
				CicsBridgeHeader.CicsBridgeTaskEndStatus.Backout,
				"Backout"
			},
			{
				CicsBridgeHeader.CicsBridgeTaskEndStatus.EndTask,
				"EndTask"
			}
		};

		// Token: 0x0400237B RID: 9083
		private static readonly Dictionary<CicsBridgeHeader.CicsBridgeStartCode, string> cicsBridgeStartCodeStrings = new Dictionary<CicsBridgeHeader.CicsBridgeStartCode, string>
		{
			{
				CicsBridgeHeader.CicsBridgeStartCode.None,
				"None"
			},
			{
				CicsBridgeHeader.CicsBridgeStartCode.Start,
				"Start"
			},
			{
				CicsBridgeHeader.CicsBridgeStartCode.StartData,
				"StartData"
			},
			{
				CicsBridgeHeader.CicsBridgeStartCode.TerminalInput,
				"TerminalInput"
			}
		};

		// Token: 0x0200093E RID: 2366
		[Flags]
		public enum CicsBridgeFlag
		{
			// Token: 0x0400237D RID: 9085
			None = 0,
			// Token: 0x0400237E RID: 9086
			PassExpiration = 1,
			// Token: 0x0400237F RID: 9087
			TruncateNulls = 2,
			// Token: 0x04002380 RID: 9088
			SyncpointOnReturn = 4
		}

		// Token: 0x0200093F RID: 2367
		public enum CicsBridgeReturnCode
		{
			// Token: 0x04002382 RID: 9090
			Ok,
			// Token: 0x04002383 RID: 9091
			CicsExecError,
			// Token: 0x04002384 RID: 9092
			ApiError,
			// Token: 0x04002385 RID: 9093
			BridgeError,
			// Token: 0x04002386 RID: 9094
			BridgeAbend,
			// Token: 0x04002387 RID: 9095
			ApplicationAbend,
			// Token: 0x04002388 RID: 9096
			SecurityError,
			// Token: 0x04002389 RID: 9097
			ProgramNotAvailable,
			// Token: 0x0400238A RID: 9098
			BridgeTimeout,
			// Token: 0x0400238B RID: 9099
			TransactionNotAvailable
		}

		// Token: 0x02000940 RID: 2368
		public enum CicsBridgeUowControl
		{
			// Token: 0x0400238D RID: 9101
			Middle = 16,
			// Token: 0x0400238E RID: 9102
			First,
			// Token: 0x0400238F RID: 9103
			Commit = 256,
			// Token: 0x04002390 RID: 9104
			Last = 272,
			// Token: 0x04002391 RID: 9105
			Only,
			// Token: 0x04002392 RID: 9106
			Backout = 4352,
			// Token: 0x04002393 RID: 9107
			Continue = 65536
		}

		// Token: 0x02000941 RID: 2369
		public enum CicsBridgeLinkType
		{
			// Token: 0x04002395 RID: 9109
			Program = 1,
			// Token: 0x04002396 RID: 9110
			Transaction
		}

		// Token: 0x02000942 RID: 2370
		public enum CicsBridgeAdsDescriptor
		{
			// Token: 0x04002398 RID: 9112
			None,
			// Token: 0x04002399 RID: 9113
			Send,
			// Token: 0x0400239A RID: 9114
			Receive = 16,
			// Token: 0x0400239B RID: 9115
			MessageFormat = 256
		}

		// Token: 0x02000943 RID: 2371
		public enum CicsBridgeTaskEndStatus
		{
			// Token: 0x0400239D RID: 9117
			NoSync,
			// Token: 0x0400239E RID: 9118
			Commit = 256,
			// Token: 0x0400239F RID: 9119
			Backout = 4352,
			// Token: 0x040023A0 RID: 9120
			EndTask = 65536
		}

		// Token: 0x02000944 RID: 2372
		public enum CicsBridgeStartCode
		{
			// Token: 0x040023A2 RID: 9122
			None,
			// Token: 0x040023A3 RID: 9123
			Start,
			// Token: 0x040023A4 RID: 9124
			StartData,
			// Token: 0x040023A5 RID: 9125
			TerminalInput
		}
	}
}
