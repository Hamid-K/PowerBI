using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular.Scripting
{
	// Token: 0x020001A7 RID: 423
	internal static class JsonCommandFactory
	{
		// Token: 0x06001A1C RID: 6684 RVA: 0x000ACBA2 File Offset: 0x000AADA2
		public static IEnumerable<JsonCommand> GetJsonSchemaSupportedCommands(CompatibilityMode mode, int dbCompatibilityLevel)
		{
			foreach (Tuple<CompatibilityRestrictionSet, JsonCommand> tuple in JsonCommandFactory.schemaSupportedCommands)
			{
				if (tuple.Item1 == null || tuple.Item1.IsCompatible(mode, dbCompatibilityLevel))
				{
					yield return tuple.Item2;
				}
			}
			IEnumerator<Tuple<CompatibilityRestrictionSet, JsonCommand>> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06001A1D RID: 6685 RVA: 0x000ACBBC File Offset: 0x000AADBC
		public static JsonCommand CreateCommand(JsonTextReader reader, CompatibilityMode mode)
		{
			reader.VerifyToken(1);
			reader.Read();
			reader.VerifyToken(4);
			string text = (string)reader.Value;
			reader.Read();
			reader.VerifyToken(1);
			JsonCommand jsonCommand;
			if (!JsonCommandFactory.TryCreateCommandByTag(text, out jsonCommand))
			{
				throw JsonSerializationUtil.CreateException(TomSR.Exception_UnrecognizedJsonCommand(text), reader, null);
			}
			jsonCommand.Parse(reader, mode);
			reader.VerifyToken(13);
			reader.Read();
			bool flag;
			try
			{
				flag = reader.Read();
			}
			catch (JsonReaderException ex)
			{
				throw JsonSerializationUtil.CreateException(TomSR.Exception_UnexpectedTokensAfterJsonCommand, ex);
			}
			if (flag)
			{
				throw JsonSerializationUtil.CreateException(TomSR.Exception_UnexpectedTokensAfterJsonCommand, reader, null);
			}
			return jsonCommand;
		}

		// Token: 0x06001A1E RID: 6686 RVA: 0x000ACC60 File Offset: 0x000AAE60
		internal static bool TryCreateCommandByTag(string tag, out JsonCommand command)
		{
			if (tag != null)
			{
				int length = tag.Length;
				switch (length)
				{
				case 5:
					if (tag == "alter")
					{
						command = new AlterJsonCommand();
						return true;
					}
					break;
				case 6:
					switch (tag[0])
					{
					case 'a':
						if (tag == "attach")
						{
							command = new AttachJsonCommand();
							return true;
						}
						break;
					case 'b':
						if (tag == "backup")
						{
							command = new BackupJsonCommand();
							return true;
						}
						break;
					case 'c':
						if (tag == "create")
						{
							command = new CreateJsonCommand();
							return true;
						}
						break;
					case 'd':
						if (tag == "delete")
						{
							command = new DeleteJsonCommand();
							return true;
						}
						if (tag == "detach")
						{
							command = new DetachJsonCommand();
							return true;
						}
						break;
					case 'e':
						if (tag == "export")
						{
							command = new ExportJsonCommand();
							return true;
						}
						break;
					}
					break;
				case 7:
				{
					char c = tag[2];
					if (c != 'f')
					{
						if (c == 's')
						{
							if (tag == "restore")
							{
								command = new RestoreJsonCommand();
								return true;
							}
						}
					}
					else if (tag == "refresh")
					{
						command = new RefreshJsonCommand();
						return true;
					}
					break;
				}
				case 8:
					if (tag == "sequence")
					{
						command = new SequenceJsonCommand();
						return true;
					}
					break;
				case 9:
				case 10:
				case 12:
				case 13:
				case 14:
					break;
				case 11:
					if (tag == "synchronize")
					{
						command = new SynchronizeJsonCommand();
						return true;
					}
					break;
				case 15:
				{
					char c = tag[0];
					if (c != 'c')
					{
						if (c == 'm')
						{
							if (tag == "mergePartitions")
							{
								command = new MergePartitionsJsonCommand();
								return true;
							}
						}
					}
					else if (tag == "createOrReplace")
					{
						command = new CreateOrReplaceJsonCommand();
						return true;
					}
					break;
				}
				default:
					if (length == 26)
					{
						if (tag == "applyAutomaticAggregations")
						{
							command = new ApplyAutomaticAggregationsJsonCommand();
							return true;
						}
					}
					break;
				}
			}
			command = null;
			return false;
		}

		// Token: 0x040004F0 RID: 1264
		private static readonly IReadOnlyList<Tuple<CompatibilityRestrictionSet, JsonCommand>> schemaSupportedCommands = new List<Tuple<CompatibilityRestrictionSet, JsonCommand>>
		{
			new Tuple<CompatibilityRestrictionSet, JsonCommand>(null, new CreateJsonCommand()),
			new Tuple<CompatibilityRestrictionSet, JsonCommand>(null, new CreateOrReplaceJsonCommand()),
			new Tuple<CompatibilityRestrictionSet, JsonCommand>(null, new AlterJsonCommand()),
			new Tuple<CompatibilityRestrictionSet, JsonCommand>(null, new DeleteJsonCommand()),
			new Tuple<CompatibilityRestrictionSet, JsonCommand>(null, new RefreshJsonCommand()),
			new Tuple<CompatibilityRestrictionSet, JsonCommand>(null, new SequenceJsonCommand()),
			new Tuple<CompatibilityRestrictionSet, JsonCommand>(null, new BackupJsonCommand()),
			new Tuple<CompatibilityRestrictionSet, JsonCommand>(null, new RestoreJsonCommand()),
			new Tuple<CompatibilityRestrictionSet, JsonCommand>(null, new AttachJsonCommand()),
			new Tuple<CompatibilityRestrictionSet, JsonCommand>(null, new DetachJsonCommand()),
			new Tuple<CompatibilityRestrictionSet, JsonCommand>(null, new SynchronizeJsonCommand()),
			new Tuple<CompatibilityRestrictionSet, JsonCommand>(null, new MergePartitionsJsonCommand()),
			new Tuple<CompatibilityRestrictionSet, JsonCommand>(CompatibilityRestrictions.Feature_AdaptiveCaching, new ApplyAutomaticAggregationsJsonCommand()),
			new Tuple<CompatibilityRestrictionSet, JsonCommand>(new CompatibilityRestrictionSet(CompatibilityMode.PowerBI), new ExportJsonCommand())
		};
	}
}
