using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.Library;
using Microsoft.Mashup.Engine1.Library.MQ;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.DataSourceReference
{
	// Token: 0x020018F3 RID: 6387
	internal sealed class MqDataSourceLocation : DataSourceLocation
	{
		// Token: 0x0600A2D1 RID: 41681 RVA: 0x0021B966 File Offset: 0x00219B66
		public MqDataSourceLocation()
		{
			base.Protocol = "ibm-mq";
		}

		// Token: 0x170029A0 RID: 10656
		// (get) Token: 0x0600A2D2 RID: 41682 RVA: 0x000E0BB2 File Offset: 0x000DEDB2
		public override string ResourceKind
		{
			get
			{
				return "MQ";
			}
		}

		// Token: 0x170029A1 RID: 10657
		// (get) Token: 0x0600A2D3 RID: 41683 RVA: 0x000E0BB2 File Offset: 0x000DEDB2
		public override string FriendlyName
		{
			get
			{
				return "MQ";
			}
		}

		// Token: 0x170029A2 RID: 10658
		// (get) Token: 0x0600A2D4 RID: 41684 RVA: 0x0021B979 File Offset: 0x00219B79
		public override IEnumerable<string> DisplayAddressFields
		{
			get
			{
				return new string[] { "server", "queueManager", "channel", "queue" };
			}
		}

		// Token: 0x0600A2D5 RID: 41685 RVA: 0x0021B9A4 File Offset: 0x00219BA4
		public override IFormulaCreationResult CreateFormula(string optionsJson)
		{
			RecordValue recordValue;
			try
			{
				recordValue = MqModule.OptionRecord.FromJson(optionsJson);
			}
			catch (ArgumentException ex)
			{
				return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidOption, ex.Message);
			}
			return DataSourceLocation.FormatInvocation("MQ.Queue", 4, new object[]
			{
				base.Address.GetStringOrNull("server"),
				base.Address.GetStringOrNull("queueManager"),
				base.Address.GetStringOrNull("channel"),
				base.Address.GetStringOrNull("queue"),
				recordValue
			});
		}

		// Token: 0x0600A2D6 RID: 41686 RVA: 0x0021BA44 File Offset: 0x00219C44
		public override bool TryGetResource(out IResource resource)
		{
			string stringOrNull = base.Address.GetStringOrNull("server");
			string stringOrNull2 = base.Address.GetStringOrNull("queueManager");
			string stringOrNull3 = base.Address.GetStringOrNull("channel");
			string stringOrNull4 = base.Address.GetStringOrNull("queue");
			if (string.IsNullOrEmpty(stringOrNull) || string.IsNullOrEmpty(stringOrNull2) || string.IsNullOrEmpty(stringOrNull3) || string.IsNullOrEmpty(stringOrNull4))
			{
				resource = null;
				return false;
			}
			resource = MqResource.New(stringOrNull, stringOrNull2, stringOrNull3, stringOrNull4);
			return true;
		}

		// Token: 0x040054DC RID: 21724
		public static readonly DataSourceLocationFactory Factory = new MqDataSourceLocation.DslFactory();

		// Token: 0x040054DD RID: 21725
		private const string MqQueueFunction = "MQ.Queue";

		// Token: 0x020018F4 RID: 6388
		private sealed class DslFactory : DataSourceLocationFactory
		{
			// Token: 0x170029A3 RID: 10659
			// (get) Token: 0x0600A2D8 RID: 41688 RVA: 0x0021BAD2 File Offset: 0x00219CD2
			public override string Protocol
			{
				get
				{
					return "ibm-mq";
				}
			}

			// Token: 0x0600A2D9 RID: 41689 RVA: 0x0021BAD9 File Offset: 0x00219CD9
			public override IDataSourceLocation New()
			{
				return new MqDataSourceLocation();
			}

			// Token: 0x0600A2DA RID: 41690 RVA: 0x0021BAE0 File Offset: 0x00219CE0
			protected override bool TryCreateFromResourcePath(string resourcePath, out IDataSourceLocation location)
			{
				string text;
				string text2;
				string text3;
				string text4;
				if (MqResource.TryParsePath(resourcePath, out text, out text2, out text3, out text4))
				{
					location = DataSourceLocationFactory.New("ibm-mq");
					location.Address = new Dictionary<string, object>
					{
						{ "server", text },
						{ "queueManager", text2 },
						{ "channel", text3 },
						{ "queue", text4 }
					};
					return true;
				}
				location = null;
				return false;
			}
		}
	}
}
