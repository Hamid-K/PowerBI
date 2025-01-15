using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.MachineLearning.Command;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Data.Conversion;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x0200013D RID: 317
	public sealed class ShowSchemaCommand : ICommand
	{
		// Token: 0x06000666 RID: 1638 RVA: 0x000222A7 File Offset: 0x000204A7
		public ShowSchemaCommand(ShowSchemaCommand.Arguments args, IHostEnvironment env)
		{
			this._impl = new ShowSchemaCommand.Impl(args, env);
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x000222BC File Offset: 0x000204BC
		public void Run()
		{
			this._impl.Run();
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x000222CC File Offset: 0x000204CC
		internal static void RunOnData(TextWriter writer, ShowSchemaCommand.Arguments args, IDataView data)
		{
			if (args.showSteps)
			{
				IEnumerable<IDataView> viewChainReversed = ShowSchemaCommand.GetViewChainReversed(data);
				using (IEnumerator<IDataView> enumerator = viewChainReversed.Reverse<IDataView>().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						IDataView dataView = enumerator.Current;
						writer.WriteLine("---- {0} ----", dataView.GetType().Name);
						ITransposeDataView transposeDataView = dataView as ITransposeDataView;
						ShowSchemaCommand.PrintSchema(writer, args, dataView.Schema, (transposeDataView == null) ? null : transposeDataView.TransposeSchema);
					}
					return;
				}
			}
			ITransposeDataView transposeDataView2 = data as ITransposeDataView;
			ShowSchemaCommand.PrintSchema(writer, args, data.Schema, (transposeDataView2 == null) ? null : transposeDataView2.TransposeSchema);
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x000224B0 File Offset: 0x000206B0
		private static IEnumerable<IDataView> GetViewChainReversed(IDataView data)
		{
			CompositeDataLoader cdl = data as CompositeDataLoader;
			IDataTransform transform;
			for (IDataView view = ((cdl != null) ? cdl.View : data); view != null; view = ((transform != null) ? transform.Source : null))
			{
				yield return view;
				transform = view as IDataTransform;
			}
			yield break;
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x000224D0 File Offset: 0x000206D0
		private static void PrintSchema(TextWriter writer, ShowSchemaCommand.Arguments args, ISchema schema, ITransposeSchema tschema)
		{
			int columnCount = schema.ColumnCount;
			writer.WriteLine("{0} columns:", columnCount);
			IndentingTextWriter indentingTextWriter = IndentingTextWriter.Wrap(writer, "  ");
			using (indentingTextWriter.Nest())
			{
				VBuffer<DvText> vbuffer = default(VBuffer<DvText>);
				for (int i = 0; i < columnCount; i++)
				{
					string columnName = schema.GetColumnName(i);
					ColumnType columnType = schema.GetColumnType(i);
					VectorType vectorType = ((tschema == null) ? null : tschema.GetSlotType(i));
					indentingTextWriter.WriteLine("{0}: {1}{2}", columnName, columnType, (vectorType == null) ? "" : " (T)");
					bool showMetadataValues = args.showMetadataValues;
					ColumnType metadataTypeOrNull;
					if (showMetadataValues || args.showMetadataTypes)
					{
						ShowSchemaCommand.ShowMetadata(indentingTextWriter, schema, i, showMetadataValues);
					}
					else if (args.showSlots && columnType.IsKnownSizeVector && (metadataTypeOrNull = schema.GetMetadataTypeOrNull("SlotNames", i)) != null && metadataTypeOrNull.VectorSize == columnType.VectorSize && metadataTypeOrNull.ItemType.IsText)
					{
						schema.GetMetadata<VBuffer<DvText>>("SlotNames", i, ref vbuffer);
						if (vbuffer.Length == columnType.VectorSize)
						{
							using (indentingTextWriter.Nest())
							{
								bool flag = args.verbose ?? false;
								foreach (KeyValuePair<int, DvText> keyValuePair in vbuffer.Items(flag))
								{
									if (flag || keyValuePair.Value.HasChars)
									{
										indentingTextWriter.WriteLine("{0}:{1}", keyValuePair.Key, keyValuePair.Value);
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x00022700 File Offset: 0x00020900
		private static void ShowMetadata(IndentingTextWriter itw, ISchema schema, int col, bool showVals)
		{
			using (itw.Nest())
			{
				foreach (KeyValuePair<string, ColumnType> keyValuePair in from p in schema.GetMetadataTypes(col)
					orderby p.Key
					select p)
				{
					ColumnType value = keyValuePair.Value;
					itw.Write("Metadata '{0}': {1}", keyValuePair.Key, value);
					if (showVals)
					{
						if (!value.IsVector)
						{
							ShowSchemaCommand.ShowMetadataValue(itw, schema, col, keyValuePair.Key, value);
						}
						else
						{
							ShowSchemaCommand.ShowMetadataValueVec(itw, schema, col, keyValuePair.Key, value);
						}
					}
					itw.WriteLine();
				}
			}
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x000227DC File Offset: 0x000209DC
		private static void ShowMetadataValue(IndentingTextWriter itw, ISchema schema, int col, string kind, ColumnType type)
		{
			if (!type.IsStandardScalar && !type.IsKey)
			{
				itw.Write(": Can't display value of this type");
				return;
			}
			Action<IndentingTextWriter, ISchema, int, string, ColumnType> action = new Action<IndentingTextWriter, ISchema, int, string, ColumnType>(ShowSchemaCommand.ShowMetadataValue<int>);
			MethodInfo methodInfo = action.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[] { type.RawType });
			methodInfo.Invoke(null, new object[] { itw, schema, col, kind, type });
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x00022864 File Offset: 0x00020A64
		private static void ShowMetadataValue<T>(IndentingTextWriter itw, ISchema schema, int col, string kind, ColumnType type)
		{
			ValueMapper<T, StringBuilder> stringConversion = Conversions.Instance.GetStringConversion<T>(type);
			T t = default(T);
			StringBuilder stringBuilder = null;
			schema.GetMetadata<T>(kind, col, ref t);
			stringConversion.Invoke(ref t, ref stringBuilder);
			itw.Write(": '{0}'", stringBuilder);
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x000228A8 File Offset: 0x00020AA8
		private static void ShowMetadataValueVec(IndentingTextWriter itw, ISchema schema, int col, string kind, ColumnType type)
		{
			if (!type.ItemType.IsStandardScalar && !type.ItemType.IsKey)
			{
				itw.Write(": Can't display value of this type");
				return;
			}
			Action<IndentingTextWriter, ISchema, int, string, ColumnType> action = new Action<IndentingTextWriter, ISchema, int, string, ColumnType>(ShowSchemaCommand.ShowMetadataValueVec<int>);
			MethodInfo methodInfo = action.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[] { type.ItemType.RawType });
			methodInfo.Invoke(null, new object[] { itw, schema, col, kind, type });
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x0002293C File Offset: 0x00020B3C
		private static void ShowMetadataValueVec<T>(IndentingTextWriter itw, ISchema schema, int col, string kind, ColumnType type)
		{
			ValueMapper<T, StringBuilder> stringConversion = Conversions.Instance.GetStringConversion<T>(type.ItemType);
			VBuffer<T> vbuffer = default(VBuffer<T>);
			schema.GetMetadata<VBuffer<T>>(kind, col, ref vbuffer);
			itw.Write(": Length={0}, Count={0}", vbuffer.Length, vbuffer.Count);
			using (itw.Nest())
			{
				StringBuilder stringBuilder = null;
				int num = 0;
				foreach (KeyValuePair<int, T> keyValuePair in vbuffer.Items(false))
				{
					if (num % 10 == 0)
					{
						itw.WriteLine();
					}
					else
					{
						itw.Write(", ");
					}
					T value = keyValuePair.Value;
					stringConversion.Invoke(ref value, ref stringBuilder);
					itw.Write("[{0}] '{1}'", keyValuePair.Key, stringBuilder);
					num++;
				}
			}
		}

		// Token: 0x04000343 RID: 835
		internal const string LoadName = "ShowSchema";

		// Token: 0x04000344 RID: 836
		private readonly ShowSchemaCommand.Impl _impl;

		// Token: 0x0200013E RID: 318
		public sealed class Arguments : DataCommand.ArgumentsBase
		{
			// Token: 0x04000346 RID: 838
			[Argument(0, HelpText = "Show all steps in transform chain", ShortName = "steps")]
			public bool showSteps;

			// Token: 0x04000347 RID: 839
			[Argument(0, HelpText = "Show the metadata types", ShortName = "metaTypes")]
			public bool showMetadataTypes;

			// Token: 0x04000348 RID: 840
			[Argument(0, HelpText = "Show the metadata types and values", ShortName = "meta,metaVals,metaValues")]
			public bool showMetadataValues;

			// Token: 0x04000349 RID: 841
			[Argument(0, HelpText = "Show slot names", ShortName = "slots", Hide = true)]
			public bool showSlots;

			// Token: 0x0400034A RID: 842
			[Argument(0, HelpText = "Show JSON version of the schema", ShortName = "json", Hide = true)]
			public bool showJson;
		}

		// Token: 0x0200013F RID: 319
		private sealed class Impl : DataCommand.ImplBase<ShowSchemaCommand.Arguments>
		{
			// Token: 0x06000672 RID: 1650 RVA: 0x00022A4C File Offset: 0x00020C4C
			public Impl(ShowSchemaCommand.Arguments args, IHostEnvironment env)
				: base("ShowSchemaCommand", args, env, null)
			{
			}

			// Token: 0x06000673 RID: 1651 RVA: 0x00022A70 File Offset: 0x00020C70
			public override void Run()
			{
				using (IChannel channel = this._host.Start("ShowSchema"))
				{
					this.RunCore(channel);
					channel.Done();
				}
			}

			// Token: 0x06000674 RID: 1652 RVA: 0x00022AB8 File Offset: 0x00020CB8
			private void RunCore(IChannel ch)
			{
				IDataLoader dataLoader = base.CreateAndSaveLoader("TextLoader");
				using (StringWriter stringWriter = new StringWriter())
				{
					ShowSchemaCommand.RunOnData(stringWriter, this._args, dataLoader);
					string text = stringWriter.ToString();
					ch.Info(text);
				}
			}
		}
	}
}
