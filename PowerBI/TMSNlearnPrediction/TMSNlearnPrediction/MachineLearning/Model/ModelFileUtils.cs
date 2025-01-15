using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Data;
using Microsoft.MachineLearning.Data.IO;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.TMSN.TMSNlearn;

namespace Microsoft.MachineLearning.Model
{
	// Token: 0x0200021B RID: 539
	public static class ModelFileUtils
	{
		// Token: 0x06000C00 RID: 3072 RVA: 0x00041CB8 File Offset: 0x0003FEB8
		public static IDataView LoadPipeline(IHostEnvironment env, Stream modelStream, IMultiStreamSource files, bool extractInnerPipe = false)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<Stream>(env, modelStream, "modelStream");
			Contracts.CheckValue<IMultiStreamSource>(env, files, "files");
			IDataView dataView;
			using (RepositoryReader repositoryReader = RepositoryReader.Open(modelStream, true))
			{
				dataView = ModelFileUtils.LoadPipeline(env, repositoryReader, files, extractInnerPipe);
			}
			return dataView;
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x00041D18 File Offset: 0x0003FF18
		public static IDataView LoadPipeline(IHostEnvironment env, RepositoryReader rep, IMultiStreamSource files, bool extractInnerPipe = false)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<RepositoryReader>(env, rep, "rep");
			Contracts.CheckValue<IMultiStreamSource>(env, files, "files");
			IDataView dataView2;
			using (Repository.Entry entry = rep.OpenEntry("DataLoaderModel", "Model.key"))
			{
				IDataLoader dataLoader;
				ModelLoadContext.LoadModel<IDataLoader, SignatureLoadDataLoader>(out dataLoader, rep, entry, "DataLoaderModel", new object[] { env, files });
				IDataView dataView = dataLoader;
				if (extractInnerPipe)
				{
					CompositeDataLoader compositeDataLoader = dataLoader as CompositeDataLoader;
					dataView = ((compositeDataLoader == null) ? dataLoader : compositeDataLoader.View);
				}
				dataView2 = dataView;
			}
			return dataView2;
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x00041DBC File Offset: 0x0003FFBC
		public static IDataView LoadTransforms(IHostEnvironment env, IDataView data, Stream modelStream)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<IDataView>(env, data, "data");
			Contracts.CheckValue<Stream>(env, modelStream, "modelStream");
			IDataView dataView;
			using (RepositoryReader repositoryReader = RepositoryReader.Open(modelStream, true))
			{
				using (Repository.Entry entry = repositoryReader.OpenEntryOrNull("DataLoaderModel", "Model.key"))
				{
					if (entry == null)
					{
						dataView = data;
					}
					else
					{
						ModelLoadContext modelLoadContext = new ModelLoadContext(repositoryReader, entry, "DataLoaderModel");
						dataView = CompositeDataLoader.LoadSelectedTransforms(modelLoadContext, data, env, (string x) => true);
					}
				}
			}
			return dataView;
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x00041E74 File Offset: 0x00040074
		public static IPredictor LoadPredictorOrNull(IHostEnvironment env, Stream modelStream)
		{
			Contracts.CheckValue<Stream>(modelStream, "modelStream");
			IPredictor predictor;
			using (RepositoryReader repositoryReader = RepositoryReader.Open(modelStream, true))
			{
				ModelLoadContext.LoadModelOrNull<IPredictor, SignatureLoadModel>(out predictor, repositoryReader, "Predictor", new object[] { env });
			}
			return predictor;
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x00041ECC File Offset: 0x000400CC
		public static ModelSaveContext GetDataModelSavingContext(RepositoryWriter rep)
		{
			Contracts.CheckValue<RepositoryWriter>(rep, "rep");
			return new ModelSaveContext(rep, "DataLoaderModel", "Model.key");
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x00041EEC File Offset: 0x000400EC
		public static IDataLoader LoadLoader(IHostEnvironment env, RepositoryReader rep, IMultiStreamSource files, bool loadTransforms)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<RepositoryReader>(env, rep, "rep");
			Contracts.CheckValue<IMultiStreamSource>(env, files, "files");
			Repository.Entry entry = null;
			string text = "";
			if (!loadTransforms)
			{
				entry = rep.OpenEntryOrNull(text = Path.Combine("DataLoaderModel", "Loader"), "Model.key");
			}
			if (entry == null)
			{
				entry = rep.OpenEntry(text = "DataLoaderModel", "Model.key");
			}
			Contracts.CheckDecode(env, entry != null, "Loader is not found.");
			IDataLoader dataLoader;
			using (entry)
			{
				ModelLoadContext.LoadModel<IDataLoader, SignatureLoadDataLoader>(out dataLoader, rep, entry, text, new object[] { env, files });
			}
			return dataLoader;
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x00041FAC File Offset: 0x000401AC
		public static bool TryLoadFeatureNames(out FeatureNameCollection featureNames, RepositoryReader rep)
		{
			Contracts.CheckValue<RepositoryReader>(rep, "rep");
			using (Repository.Entry entry = rep.OpenEntryOrNull("TrainingInfo", "FeatureNames.bin"))
			{
				if (entry != null)
				{
					using (ModelLoadContext modelLoadContext = new ModelLoadContext(rep, entry, "TrainingInfo"))
					{
						featureNames = FeatureNameCollection.Create(modelLoadContext);
						return true;
					}
				}
			}
			featureNames = null;
			return false;
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x0004203C File Offset: 0x0004023C
		internal static void SaveRoleMappings(IHostEnvironment env, IChannel ch, RoleMappedSchema schema, RepositoryWriter rep)
		{
			ArrayDataViewBuilder arrayDataViewBuilder = new ArrayDataViewBuilder(env);
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			foreach (KeyValuePair<RoleMappedSchema.ColumnRole, string> keyValuePair in from r in schema.GetColumnRoleNames()
				orderby r.Key.Value
				select r)
			{
				list.Add(keyValuePair.Key.Value);
				list2.Add(keyValuePair.Value);
			}
			arrayDataViewBuilder.AddColumn("Role", list.ToArray());
			arrayDataViewBuilder.AddColumn("Column", list2.ToArray());
			using (Repository.Entry entry = rep.CreateEntry("TrainingInfo", "RoleMapping.txt"))
			{
				TextSaver textSaver = new TextSaver(new TextSaver.Arguments
				{
					dense = true,
					silent = true
				}, env);
				IDataView dataView = arrayDataViewBuilder.GetDataView(null);
				textSaver.SaveData(entry.Stream, dataView, Utils.GetIdentityPermutation(dataView.Schema.ColumnCount));
			}
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x00042180 File Offset: 0x00040380
		public static IEnumerable<KeyValuePair<RoleMappedSchema.ColumnRole, string>> LoadRoleMappingsOrNull(IHostEnvironment env, Stream modelStream)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<Stream>(env, modelStream, "modelStream");
			IEnumerable<KeyValuePair<RoleMappedSchema.ColumnRole, string>> enumerable;
			using (RepositoryReader repositoryReader = RepositoryReader.Open(modelStream, true))
			{
				enumerable = ModelFileUtils.LoadRoleMappingsOrNull(env, repositoryReader);
			}
			return enumerable;
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x000421D8 File Offset: 0x000403D8
		public static IEnumerable<KeyValuePair<RoleMappedSchema.ColumnRole, string>> LoadRoleMappingsOrNull(IHostEnvironment env, RepositoryReader rep)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost host = env.Register("RoleMappingUtils");
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			Repository.Entry entry = rep.OpenEntryOrNull("TrainingInfo", "RoleMapping.txt");
			if (entry == null)
			{
				return null;
			}
			entry.Dispose();
			using (IChannel channel = host.Start("Loading role mappings"))
			{
				SubComponent<IDataLoader, SignatureDataLoader> subComponent = new SubComponent<IDataLoader, SignatureDataLoader>("Text");
				IDataLoader dataLoader = ComponentCatalog.CreateInstance<IDataLoader, SignatureDataLoader>(subComponent, new object[]
				{
					env,
					new ModelFileUtils.RepositoryStreamWrapper(rep, "TrainingInfo", "RoleMapping.txt")
				});
				using (IRowCursor rowCursor = dataLoader.GetRowCursor((int c) => true, null))
				{
					ValueGetter<DvText> getter = rowCursor.GetGetter<DvText>(0);
					ValueGetter<DvText> getter2 = rowCursor.GetGetter<DvText>(1);
					DvText dvText = default(DvText);
					DvText dvText2 = default(DvText);
					while (rowCursor.MoveNext())
					{
						getter.Invoke(ref dvText);
						getter2.Invoke(ref dvText2);
						string text = dvText.ToString();
						string text2 = dvText2.ToString();
						Contracts.CheckDecode(host, !string.IsNullOrWhiteSpace(text), "Role name must not be empty");
						Contracts.CheckDecode(host, !string.IsNullOrWhiteSpace(text2), "Column name must not be empty");
						list.Add(new KeyValuePair<string, string>(text, text2));
					}
				}
				channel.Done();
			}
			return TrainUtils.CheckAndGenerateCustomColumns(env, list.ToArray());
		}

		// Token: 0x040006A2 RID: 1698
		public const string DirPredictor = "Predictor";

		// Token: 0x040006A3 RID: 1699
		internal const string DirDataLoaderModel = "DataLoaderModel";

		// Token: 0x040006A4 RID: 1700
		public const string DirTrainingInfo = "TrainingInfo";

		// Token: 0x040006A5 RID: 1701
		private const string RoleMappingFile = "RoleMapping.txt";

		// Token: 0x0200021C RID: 540
		private sealed class RepositoryStreamWrapper : IMultiStreamSource
		{
			// Token: 0x06000C0D RID: 3085 RVA: 0x00042384 File Offset: 0x00040584
			public RepositoryStreamWrapper(RepositoryReader repository, string directory, string filename)
			{
				Contracts.CheckValue<RepositoryReader>(repository, "repository");
				Contracts.CheckNonWhiteSpace(directory, "directory");
				Contracts.CheckNonWhiteSpace(filename, "filename");
				this._repository = repository;
				this._directory = directory;
				this._filename = filename;
			}

			// Token: 0x1700015E RID: 350
			// (get) Token: 0x06000C0E RID: 3086 RVA: 0x000423C4 File Offset: 0x000405C4
			public int Count
			{
				get
				{
					return 1;
				}
			}

			// Token: 0x06000C0F RID: 3087 RVA: 0x000423C7 File Offset: 0x000405C7
			public string GetPathOrNull(int index)
			{
				Contracts.Check(index == 0);
				return null;
			}

			// Token: 0x06000C10 RID: 3088 RVA: 0x000423D4 File Offset: 0x000405D4
			public Stream Open(int index)
			{
				Repository.Entry entry = this._repository.OpenEntryOrNull(this._directory, this._filename);
				if (entry == null)
				{
					throw Contracts.ExceptValue("filename", "File '{0}' is missing from the repository", new object[] { this._filename });
				}
				return new ModelFileUtils.RepositoryStreamWrapper.EntryStream(entry);
			}

			// Token: 0x06000C11 RID: 3089 RVA: 0x00042423 File Offset: 0x00040623
			public TextReader OpenTextReader(int index)
			{
				return new StreamReader(this.Open(index));
			}

			// Token: 0x040006A9 RID: 1705
			private readonly RepositoryReader _repository;

			// Token: 0x040006AA RID: 1706
			private readonly string _directory;

			// Token: 0x040006AB RID: 1707
			private readonly string _filename;

			// Token: 0x0200021D RID: 541
			private sealed class EntryStream : Stream
			{
				// Token: 0x1700015F RID: 351
				// (get) Token: 0x06000C12 RID: 3090 RVA: 0x00042431 File Offset: 0x00040631
				public override bool CanRead
				{
					get
					{
						return this._entry.Stream.CanRead;
					}
				}

				// Token: 0x17000160 RID: 352
				// (get) Token: 0x06000C13 RID: 3091 RVA: 0x00042443 File Offset: 0x00040643
				public override bool CanSeek
				{
					get
					{
						return this._entry.Stream.CanSeek;
					}
				}

				// Token: 0x17000161 RID: 353
				// (get) Token: 0x06000C14 RID: 3092 RVA: 0x00042455 File Offset: 0x00040655
				public override bool CanWrite
				{
					get
					{
						return this._entry.Stream.CanWrite;
					}
				}

				// Token: 0x17000162 RID: 354
				// (get) Token: 0x06000C15 RID: 3093 RVA: 0x00042467 File Offset: 0x00040667
				public override long Length
				{
					get
					{
						return this._entry.Stream.Length;
					}
				}

				// Token: 0x17000163 RID: 355
				// (get) Token: 0x06000C16 RID: 3094 RVA: 0x00042479 File Offset: 0x00040679
				// (set) Token: 0x06000C17 RID: 3095 RVA: 0x0004248B File Offset: 0x0004068B
				public override long Position
				{
					get
					{
						return this._entry.Stream.Position;
					}
					set
					{
						this._entry.Stream.Position = value;
					}
				}

				// Token: 0x06000C18 RID: 3096 RVA: 0x0004249E File Offset: 0x0004069E
				public EntryStream(Repository.Entry entry)
				{
					Contracts.CheckValue<Repository.Entry>(entry, "entry");
					Contracts.CheckValue<Stream>(entry.Stream, "entry.Stream");
					this._entry = entry;
				}

				// Token: 0x06000C19 RID: 3097 RVA: 0x000424C8 File Offset: 0x000406C8
				public override void Flush()
				{
					this._entry.Stream.Flush();
				}

				// Token: 0x06000C1A RID: 3098 RVA: 0x000424DA File Offset: 0x000406DA
				public override long Seek(long offset, SeekOrigin origin)
				{
					return this._entry.Stream.Seek(offset, origin);
				}

				// Token: 0x06000C1B RID: 3099 RVA: 0x000424EE File Offset: 0x000406EE
				public override void SetLength(long value)
				{
					this._entry.Stream.SetLength(value);
				}

				// Token: 0x06000C1C RID: 3100 RVA: 0x00042501 File Offset: 0x00040701
				public override int Read(byte[] buffer, int offset, int count)
				{
					return this._entry.Stream.Read(buffer, offset, count);
				}

				// Token: 0x06000C1D RID: 3101 RVA: 0x00042516 File Offset: 0x00040716
				public override void Write(byte[] buffer, int offset, int count)
				{
					this._entry.Stream.Write(buffer, offset, count);
				}

				// Token: 0x06000C1E RID: 3102 RVA: 0x0004252B File Offset: 0x0004072B
				protected override void Dispose(bool disposing)
				{
					if (!this._disposed)
					{
						this._entry.Dispose();
						this._disposed = true;
					}
					base.Dispose(disposing);
				}

				// Token: 0x06000C1F RID: 3103 RVA: 0x0004254E File Offset: 0x0004074E
				[Conditional("DEBUG")]
				private void AssertValid()
				{
				}

				// Token: 0x040006AC RID: 1708
				private bool _disposed;

				// Token: 0x040006AD RID: 1709
				private readonly Repository.Entry _entry;
			}
		}
	}
}
