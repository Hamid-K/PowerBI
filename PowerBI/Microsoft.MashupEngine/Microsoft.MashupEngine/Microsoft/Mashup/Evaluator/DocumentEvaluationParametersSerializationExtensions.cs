using System;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001C8D RID: 7309
	internal static class DocumentEvaluationParametersSerializationExtensions
	{
		// Token: 0x0600B5E1 RID: 46561 RVA: 0x0024EDE4 File Offset: 0x0024CFE4
		public static void WriteDocumentEvaluationParameters(this BinaryWriter writer, DocumentEvaluationParameters parameters)
		{
			writer.WriteDocumentEvaluationConfig(parameters.config);
			writer.WriteNullable(parameters.firewallPlan, delegate(BinaryWriter w, IFirewallPlan fwp)
			{
				writer.WriteIFirewallPlan(fwp);
			});
			writer.WriteNullable(parameters.partitionKey, delegate(BinaryWriter w, IPartitionKey p)
			{
				writer.WriteIPartitionKey(p);
			});
			writer.WriteNullableString(parameters.expression);
			writer.WriteBool(parameters.executeAction);
			writer.WriteBool(parameters.reportRelationships);
		}

		// Token: 0x0600B5E2 RID: 46562 RVA: 0x0024EE7C File Offset: 0x0024D07C
		public static DocumentEvaluationParameters ReadDocumentEvaluationParameters(this BinaryReader reader)
		{
			DocumentEvaluationConfig documentEvaluationConfig = reader.ReadDocumentEvaluationConfig();
			IFirewallPlan firewallPlan = reader.ReadNullable((BinaryReader r) => reader.ReadIFirewallPlan());
			IPartitionKey partitionKey = reader.ReadNullable((BinaryReader r) => reader.ReadIPartitionKey());
			string text = reader.ReadNullableString();
			bool flag = reader.ReadBool();
			bool flag2 = reader.ReadBool();
			return new DocumentEvaluationParameters
			{
				config = documentEvaluationConfig,
				document = null,
				firewallPlan = firewallPlan,
				partitionKey = partitionKey,
				expression = text,
				executeAction = flag,
				reportRelationships = flag2
			};
		}

		// Token: 0x0600B5E3 RID: 46563 RVA: 0x0024EF32 File Offset: 0x0024D132
		public static void WriteDocumentEvaluationConfig(this BinaryWriter writer, DocumentEvaluationConfig config)
		{
			writer.WriteBool(config.debug);
			writer.WriteBool(config.enableFirewall);
			writer.WriteArray(config.requiredModules, new Action<BinaryWriter, IModule>(ModuleStub.Write));
		}

		// Token: 0x0600B5E4 RID: 46564 RVA: 0x0024EF64 File Offset: 0x0024D164
		public static DocumentEvaluationConfig ReadDocumentEvaluationConfig(this BinaryReader reader)
		{
			bool flag = reader.ReadBool();
			bool flag2 = reader.ReadBool();
			IModule[] array = reader.ReadArray((BinaryReader r) => new ModuleStub(r));
			IModule[] array2 = array;
			return new DocumentEvaluationConfig
			{
				debug = flag,
				enableFirewall = flag2,
				requiredModules = array2
			};
		}
	}
}
