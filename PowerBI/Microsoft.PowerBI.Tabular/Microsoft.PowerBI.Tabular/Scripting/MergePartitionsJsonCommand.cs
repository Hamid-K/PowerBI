using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AnalysisServices.Hosting;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular.Scripting
{
	// Token: 0x020001AA RID: 426
	internal sealed class MergePartitionsJsonCommand : JsonCommand
	{
		// Token: 0x06001A24 RID: 6692 RVA: 0x000ACED8 File Offset: 0x000AB0D8
		public MergePartitionsJsonCommand(Partition targetPartition, IEnumerable<Partition> sourcePartitions)
			: this()
		{
			this.targetPartitionPath = ObjectPath.CreateDbQualifiedPath(targetPartition.GetPath(null), targetPartition.Model.Database.Name);
			foreach (Partition partition in sourcePartitions)
			{
				this.sourcePartitionNames.Add(partition.Name);
			}
			this.isCommandValid = true;
		}

		// Token: 0x06001A25 RID: 6693 RVA: 0x000ACF5C File Offset: 0x000AB15C
		internal MergePartitionsJsonCommand()
			: base(JsonCommandType.MergePartitions, "mergePartitions", "MergePartitions", true, false, false, false)
		{
			this.sourcePartitionNames = new List<string>();
			base.ObjectType = ObjectType.Partition;
		}

		// Token: 0x06001A26 RID: 6694 RVA: 0x000ACF88 File Offset: 0x000AB188
		internal override void ApplyChangesLocally(Server server, Database activeDB)
		{
			Utils.Verify(activeDB != null);
			Model model = activeDB.Model;
			Partition partition = ObjectTreeHelper.LocateObjectByPath(this.targetPartitionPath, model) as Partition;
			if (partition == null)
			{
				throw new TomException(TomSR.Exception_JsonCommandMergePartitionsCantFindTargetPartition(base.CommandName));
			}
			Table table = partition.Table;
			List<Partition> list = new List<Partition>();
			foreach (string text in this.sourcePartitionNames)
			{
				Utils.Verify(!string.IsNullOrEmpty(text), "Name of source partition is empty. This must be validated during parsing.");
				Partition partition2 = table.Partitions.Find(text);
				if (partition2 == null)
				{
					throw new TomException(TomSR.Exception_JsonCommandMergePartitionsCantFindSourcePartition(base.CommandName, ClientHostingManager.MarkAsRestrictedInformation(text, InfoRestrictionType.CCON)));
				}
				list.Add(partition2);
			}
			partition.RequestMerge(list);
			this.modelToSave = model;
		}

		// Token: 0x06001A27 RID: 6695 RVA: 0x000AD070 File Offset: 0x000AB270
		internal override XmlaScript GenerateXmlaScript(Server server)
		{
			Utils.Verify(!server.CaptureXml && server.CaptureLog.Count == 0, "Server must not be in capturing mode, and can't have any cached XMLA commands!");
			XmlaScript xmlaScript = new XmlaScript();
			try
			{
				server.CaptureXml = true;
				this.modelToSave.SaveChangesImpl(SaveFlags.Default, 0);
				for (int i = 0; i < server.CaptureLog.Count; i++)
				{
					xmlaScript.Commands.Add(new XmlaCommand
					{
						CommandText = server.CaptureLog[i],
						DatabaseName = this.modelToSave.Database.Name,
						IsTabular = true
					});
				}
			}
			finally
			{
				server.CaptureXml = false;
				server.CaptureLog.Clear();
			}
			return xmlaScript;
		}

		// Token: 0x06001A28 RID: 6696 RVA: 0x000AD138 File Offset: 0x000AB338
		private protected override void ParseProperties(JsonTextReader reader, CompatibilityMode mode)
		{
			while (reader.TokenType != 13)
			{
				reader.VerifyToken(4);
				string text = (string)reader.Value;
				reader.Read();
				if (!(text == "target"))
				{
					if (!(text == "sources"))
					{
						throw JsonSerializationUtil.CreateException(TomSR.Exception_UnexpectedJsonProperty(text), reader, null);
					}
					reader.VerifyToken(2);
					reader.Read();
					while (reader.TokenType == 9)
					{
						if (string.IsNullOrEmpty((string)reader.Value))
						{
							throw JsonSerializationUtil.CreateException(TomSR.Exception_JsonCommandMergePartitionsSourceNameIsEmpty(base.CommandName), reader, null);
						}
						this.sourcePartitionNames.Add((string)reader.Value);
						reader.Read();
					}
					reader.VerifyToken(14);
					reader.Read();
					if (this.sourcePartitionNames.Count == 0)
					{
						throw JsonSerializationUtil.CreateException(TomSR.Exception_JsonCommandMergePartitionsNoSourcePartitions(base.CommandName), reader, null);
					}
				}
				else
				{
					reader.VerifyToken(1);
					this.targetPartitionPath = ObjectPath.Parse(reader);
					reader.VerifyToken(13);
					reader.Read();
				}
			}
		}

		// Token: 0x06001A29 RID: 6697 RVA: 0x000AD250 File Offset: 0x000AB450
		private protected override void ValidateProperties()
		{
			if (this.targetPartitionPath == null || this.targetPartitionPath.IsEmpty)
			{
				throw JsonSerializationUtil.CreateException(TomSR.Exception_JsonCommandMergePartitionsNoTargetPartition(base.CommandName), null);
			}
			if (this.sourcePartitionNames.Count == 0)
			{
				throw JsonSerializationUtil.CreateException(TomSR.Exception_JsonCommandMergePartitionsNoSourcePartitions(base.CommandName), null);
			}
		}

		// Token: 0x06001A2A RID: 6698 RVA: 0x000AD2A4 File Offset: 0x000AB4A4
		private protected override void WriteProperties(JsonWriter writer)
		{
			writer.WritePropertyName("target");
			this.targetPartitionPath.Write(writer);
			writer.WritePropertyName("sources");
			writer.WriteStartArray();
			foreach (string text in this.sourcePartitionNames)
			{
				writer.WriteValue(text);
			}
			writer.WriteEndArray();
		}

		// Token: 0x06001A2B RID: 6699 RVA: 0x000AD328 File Offset: 0x000AB528
		private protected override void WritePropertiesSchema(JsonWriter writer, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			writer.WritePropertyName("target");
			JsonSchemaWriter.WriteSchemaForObjectPath(writer, ObjectType.Partition, true);
			writer.WritePropertyName("sources");
			JsonSchemaWriter.WriteSchemaForStringArray(writer, new int?(1), true);
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x000AD358 File Offset: 0x000AB558
		private protected override string GetAffectedDatabaseName()
		{
			return this.targetPartitionPath.SingleOrDefault((KeyValuePair<ObjectType, string> e) => e.Key == ObjectType.Database).Value;
		}

		// Token: 0x04000503 RID: 1283
		private ObjectPath targetPartitionPath;

		// Token: 0x04000504 RID: 1284
		private List<string> sourcePartitionNames;

		// Token: 0x04000505 RID: 1285
		private Model modelToSave;
	}
}
