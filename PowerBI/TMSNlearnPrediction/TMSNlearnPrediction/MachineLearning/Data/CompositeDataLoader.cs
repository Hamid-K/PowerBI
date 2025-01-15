using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000384 RID: 900
	public sealed class CompositeDataLoader : IDataLoader, ICanSaveModel, ITransposeDataView, IDataView, ISchematized
	{
		// Token: 0x06001368 RID: 4968 RVA: 0x0006D1E5 File Offset: 0x0006B3E5
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("PIPELODR", 65538U, 65538U, 65537U, "PipeDataLoader", null);
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06001369 RID: 4969 RVA: 0x0006D206 File Offset: 0x0006B406
		internal IDataView View
		{
			get
			{
				return this._view;
			}
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x0006D210 File Offset: 0x0006B410
		public static IDataLoader Create(CompositeDataLoader.Arguments args, IHostEnvironment env, IMultiStreamSource files)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost host = env.Register("Composite");
			Contracts.CheckValue<CompositeDataLoader.Arguments>(host, args, "args");
			Contracts.CheckUserArg(host, SubComponentExtensions.IsGood(args.loader), "loader");
			Contracts.CheckValue<IMultiStreamSource>(host, files, "files");
			IDataLoader dataLoader = ComponentCatalog.CreateInstance<IDataLoader, SignatureDataLoader>(args.loader, new object[] { host, files });
			return CompositeDataLoader.CreateCore(host, dataLoader, args.transform);
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x0006D28C File Offset: 0x0006B48C
		public static IDataLoader Create(IHostEnvironment env, IDataLoader srcLoader, params KeyValuePair<string, SubComponent<IDataTransform, SignatureDataTransform>>[] transformArgs)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost host = env.Register("Composite");
			Contracts.CheckValue<IDataLoader>(host, srcLoader, "srcLoader");
			return CompositeDataLoader.CreateCore(host, srcLoader, transformArgs);
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x0006D34C File Offset: 0x0006B54C
		private static IDataLoader CreateCore(IHost host, IDataLoader srcLoader, KeyValuePair<string, SubComponent<IDataTransform, SignatureDataTransform>>[] transformArgs)
		{
			if (Utils.Size<KeyValuePair<string, SubComponent<IDataTransform, SignatureDataTransform>>>(transformArgs) == 0)
			{
				return srcLoader;
			}
			KeyValuePair<string, string>[] array = transformArgs.Select((KeyValuePair<string, SubComponent<IDataTransform, SignatureDataTransform>> x) => new KeyValuePair<string, string>(x.Key, x.Value.ToString())).ToArray<KeyValuePair<string, string>>();
			CompositeDataLoader compositeDataLoader = srcLoader as CompositeDataLoader;
			if (compositeDataLoader != null)
			{
				using (IChannel channel = host.Start("TagValidation"))
				{
					KeyValuePair<string, string>[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						KeyValuePair<string, string> pair = array2[i];
						KeyValuePair<string, string> pair4 = pair;
						if (!string.IsNullOrEmpty(pair4.Key))
						{
							if (compositeDataLoader._transforms.Any(delegate(CompositeDataLoader.TransformEx x)
							{
								string tag = x.Tag;
								KeyValuePair<string, string> pair3 = pair;
								return tag == pair3.Key;
							}))
							{
								IChannel channel2 = channel;
								string text = "The transform with tag '{0}' already exists in the chain";
								object[] array3 = new object[1];
								object[] array4 = array3;
								int num = 0;
								KeyValuePair<string, string> pair2 = pair;
								array4[num] = pair2.Key;
								channel2.Warning(text, array3);
							}
						}
					}
					channel.Done();
				}
			}
			return CompositeDataLoader.ApplyTransformsCore(host, srcLoader, array, (IHostEnvironment prov, int index, IDataView data) => ComponentCatalog.CreateInstance<IDataTransform, SignatureDataTransform>(transformArgs[index].Value, new object[] { prov, data }));
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x0006D49C File Offset: 0x0006B69C
		public static IDataLoader ApplyTransforms(IHostEnvironment env, IDataLoader srcLoader, KeyValuePair<string, string>[] tagData, Func<IHostEnvironment, int, IDataView, IDataView> createTransform)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost host = env.Register("Composite");
			Contracts.CheckValue<IDataLoader>(host, srcLoader, "srcLoader");
			Contracts.CheckValue<Func<IHostEnvironment, int, IDataView, IDataView>>(host, createTransform, "createTransform");
			if (Utils.Size<KeyValuePair<string, string>>(tagData) == 0)
			{
				return srcLoader;
			}
			return CompositeDataLoader.ApplyTransformsCore(host, srcLoader, tagData, createTransform);
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x0006D504 File Offset: 0x0006B704
		private static IDataLoader ApplyTransformsCore(IHost host, IDataLoader srcLoader, KeyValuePair<string, string>[] tagData, Func<IHostEnvironment, int, IDataView, IDataView> createTransform)
		{
			List<CompositeDataLoader.TransformEx> list = new List<CompositeDataLoader.TransformEx>();
			CompositeDataLoader compositeDataLoader = srcLoader as CompositeDataLoader;
			IDataView dataView;
			IDataLoader dataLoader;
			if (compositeDataLoader != null)
			{
				dataView = compositeDataLoader._view;
				list.AddRange(compositeDataLoader._transforms);
				dataLoader = compositeDataLoader._loader;
			}
			else
			{
				dataLoader = srcLoader;
				dataView = srcLoader;
			}
			IDataView dataView2 = dataView;
			using (IChannel channel = host.Start("Transforms"))
			{
				int num = Utils.Size<KeyValuePair<string, string>>(tagData);
				List<CompositeDataLoader.TransformEx> list2 = new List<CompositeDataLoader.TransformEx>();
				int i = 0;
				while (i < num)
				{
					string text = tagData[i].Key;
					if (string.IsNullOrEmpty(text))
					{
						text = CompositeDataLoader.GenerateTag(list.Count);
					}
					IDataView dataView3 = createTransform(host, i, dataView2);
					list2.Clear();
					IDataView dataView4 = dataView3;
					int num2;
					for (;;)
					{
						IDataTransform cur = dataView4 as IDataTransform;
						if (cur == null)
						{
							goto Block_6;
						}
						num2 = list.FindLastIndex((CompositeDataLoader.TransformEx x) => x.Transform == cur);
						if (num2 >= 0)
						{
							goto Block_7;
						}
						list2.Add(new CompositeDataLoader.TransformEx(text, tagData[i].Value, cur));
						dataView4 = cur.Source;
					}
					IL_013D:
					list2.Reverse();
					list.AddRange(list2);
					dataView2 = dataView3;
					i++;
					continue;
					Block_6:
					Contracts.Check(channel, dataView4 == dataLoader, "The transform has corrupted the chain (chain no longer starts with the same loader).");
					list.Clear();
					goto IL_013D;
					Block_7:
					if (num2 < list.Count - 1)
					{
						list.RemoveRange(num2 + 1, list.Count - num2 - 1);
						goto IL_013D;
					}
					goto IL_013D;
				}
				channel.Done();
			}
			if (dataView2 != dataView)
			{
				return new CompositeDataLoader(host, list.ToArray());
			}
			return srcLoader;
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x0006D6CC File Offset: 0x0006B8CC
		public static IDataLoader ApplyTransform(IHostEnvironment env, IDataLoader srcLoader, string tag, string creationArgs, Func<IHostEnvironment, IDataView, IDataView> createTransform)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost host = env.Register("Composite");
			Contracts.CheckValue<IDataLoader>(host, srcLoader, "srcLoader");
			Contracts.CheckValue<Func<IHostEnvironment, IDataView, IDataView>>(host, createTransform, "createTransform");
			KeyValuePair<string, string>[] array = new KeyValuePair<string, string>[]
			{
				new KeyValuePair<string, string>(tag, creationArgs)
			};
			return CompositeDataLoader.ApplyTransformsCore(env.Register("Composite"), srcLoader, array, (IHostEnvironment e, int index, IDataView data) => createTransform(e, data));
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x0006D758 File Offset: 0x0006B958
		public static IDataLoader Create(ModelLoadContext ctx, IHostEnvironment env, IMultiStreamSource files)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost host = env.Register("Composite");
			Contracts.CheckValue<ModelLoadContext>(host, ctx, "ctx");
			ctx.CheckAtModel(CompositeDataLoader.GetVersionInfo());
			Contracts.CheckValue<IMultiStreamSource>(host, files, "files");
			IDataLoader dataLoader3;
			using (IChannel channel = host.Start("Components"))
			{
				IDataLoader dataLoader;
				ctx.LoadModel<IDataLoader, SignatureLoadDataLoader>(out dataLoader, "Loader", new object[] { host, files });
				IDataLoader dataLoader2 = CompositeDataLoader.LoadTransforms(ctx, dataLoader, host, (string x) => true);
				channel.Done();
				dataLoader3 = dataLoader2;
			}
			return dataLoader3;
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x0006D81C File Offset: 0x0006BA1C
		public static IDataLoader Create(ModelLoadContext ctx, IHostEnvironment env, IDataLoader srcLoader, Func<string, bool> isTransformTagAccepted)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost host = env.Register("Composite");
			Contracts.CheckValue<ModelLoadContext>(host, ctx, "ctx");
			ctx.CheckAtModel(CompositeDataLoader.GetVersionInfo());
			Contracts.CheckValue<IDataLoader>(host, srcLoader, "srcView");
			Contracts.CheckValue<Func<string, bool>>(host, isTransformTagAccepted, "isTransformTagAccepted");
			return CompositeDataLoader.LoadTransforms(ctx, srcLoader, host, isTransformTagAccepted);
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x0006D878 File Offset: 0x0006BA78
		public static IDataView LoadSelectedTransforms(ModelLoadContext ctx, IDataView srcView, IHostEnvironment env, Func<string, bool> isTransformTagAccepted)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost host = env.Register("Composite");
			Contracts.CheckValue<ModelLoadContext>(host, ctx, "ctx");
			Contracts.Check(host, ctx.Reader.BaseStream.Position == ctx.FpMin + ctx.Header.FpModel);
			VersionInfo versionInfo = CompositeDataLoader.GetVersionInfo();
			if (ctx.Header.ModelSignature != versionInfo.ModelSignature)
			{
				using (IChannel channel = host.Start("ModelCheck"))
				{
					channel.Info("The data model doesn't contain transforms.");
					channel.Done();
				}
				return srcView;
			}
			ModelHeader.CheckVersionInfo(ref ctx.Header, versionInfo);
			Contracts.CheckValue<IDataView>(host, srcView, "srcView");
			Contracts.CheckValue<Func<string, bool>>(host, isTransformTagAccepted, "isTransformTagAccepted");
			int num = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(host, num == 4);
			int num2 = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(host, num2 >= 0);
			bool flag = ctx.Header.ModelVerReadable >= 65538U;
			IDataView dataView = srcView;
			for (int i = 0; i < num2; i++)
			{
				string text = "";
				if (flag)
				{
					text = ctx.LoadNonEmptyString();
					ctx.LoadStringOrNull();
				}
				if (isTransformTagAccepted(text))
				{
					IDataTransform dataTransform;
					ctx.LoadModel<IDataTransform, SignatureLoadDataTransform>(out dataTransform, string.Format("Transform_{0:000}", i), new object[] { env, dataView });
					dataView = dataTransform;
				}
			}
			return dataView;
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x0006DA00 File Offset: 0x0006BC00
		private CompositeDataLoader(IHost host, CompositeDataLoader.TransformEx[] transforms)
		{
			this._host = host;
			this._view = transforms[transforms.Length - 1].Transform;
			this._tview = this._view as ITransposeDataView;
			this._tschema = ((this._tview == null) ? new TransposerUtils.SimpleTransposeSchema(this._view.Schema) : this._tview.TransposeSchema);
			IDataLoader dataLoader = transforms[0].Transform.Source as IDataLoader;
			this._loader = dataLoader;
			this._transforms = transforms;
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x0006DAF0 File Offset: 0x0006BCF0
		private static IDataLoader LoadTransforms(ModelLoadContext ctx, IDataLoader srcLoader, IHost host, Func<string, bool> isTransformTagAccepted)
		{
			int num = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(host, num == 4);
			int num2 = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(host, num2 >= 0);
			bool flag = ctx.Header.ModelVerReadable >= 65538U;
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			List<int> acceptedIds = new List<int>();
			for (int i = 0; i < num2; i++)
			{
				string text = "";
				string text2 = null;
				if (flag)
				{
					text = ctx.LoadNonEmptyString();
					text2 = ctx.LoadStringOrNull();
				}
				if (isTransformTagAccepted(text))
				{
					acceptedIds.Add(i);
					list.Add(new KeyValuePair<string, string>(text, text2));
				}
			}
			if (list.Count == 0)
			{
				return srcLoader;
			}
			return CompositeDataLoader.ApplyTransformsCore(host, srcLoader, list.ToArray(), delegate(IHostEnvironment h, int index, IDataView data)
			{
				IDataTransform dataTransform;
				ctx.LoadModel<IDataTransform, SignatureLoadDataTransform>(out dataTransform, string.Format("Transform_{0:000}", acceptedIds[index]), new object[] { host, data });
				return dataTransform;
			});
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x0006DC14 File Offset: 0x0006BE14
		public void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(CompositeDataLoader.GetVersionInfo());
			CompositeDataLoader.SaveCore(ctx, new Action<ModelSaveContext>(this._loader.Save), this._transforms);
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x0006DC70 File Offset: 0x0006BE70
		public static void SavePipe(IHostEnvironment env, ModelSaveContext ctx, Action<ModelSaveContext> loaderSaveAction, IList<IDataTransform> transforms)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost host = env.Register("Composite");
			Contracts.CheckValue<ModelSaveContext>(host, ctx, "ctx");
			Contracts.CheckValue<Action<ModelSaveContext>>(host, loaderSaveAction, "loaderSaveAction");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(CompositeDataLoader.GetVersionInfo());
			CompositeDataLoader.TransformEx[] array = transforms.Select((IDataTransform xf, int i) => new CompositeDataLoader.TransformEx(CompositeDataLoader.GenerateTag(i), null, xf)).ToArray<CompositeDataLoader.TransformEx>();
			CompositeDataLoader.SaveCore(ctx, loaderSaveAction, array);
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x0006DCF0 File Offset: 0x0006BEF0
		private static void SaveCore(ModelSaveContext ctx, Action<ModelSaveContext> loaderSaveAction, CompositeDataLoader.TransformEx[] transforms)
		{
			ctx.Writer.Write(4);
			ctx.Writer.Write(transforms.Length);
			using (ModelSaveContext modelSaveContext = new ModelSaveContext(ctx.Repository, Path.Combine(ctx.Directory ?? "", "Loader"), "Model.key"))
			{
				loaderSaveAction(modelSaveContext);
				modelSaveContext.Done();
			}
			for (int i = 0; i < transforms.Length; i++)
			{
				string text = string.Format("Transform_{0:000}", i);
				ctx.SaveModel<IDataTransform>(transforms[i].Transform, text);
				ctx.SaveNonEmptyString(transforms[i].Tag);
				ctx.SaveStringOrNull(transforms[i].ArgsString);
			}
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x0006DDD0 File Offset: 0x0006BFD0
		private static string GenerateTag(int index)
		{
			return string.Format("xf{0:00}", index);
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x0006DDE2 File Offset: 0x0006BFE2
		public long? GetRowCount(bool lazy = true)
		{
			return this._view.GetRowCount(lazy);
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x0600137A RID: 4986 RVA: 0x0006DDF0 File Offset: 0x0006BFF0
		public bool CanShuffle
		{
			get
			{
				return this._view.CanShuffle;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x0600137B RID: 4987 RVA: 0x0006DDFD File Offset: 0x0006BFFD
		public ISchema Schema
		{
			get
			{
				return this._view.Schema;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x0600137C RID: 4988 RVA: 0x0006DE0A File Offset: 0x0006C00A
		public ITransposeSchema TransposeSchema
		{
			get
			{
				return this._tschema;
			}
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x0006DE12 File Offset: 0x0006C012
		public IRowCursor GetRowCursor(Func<int, bool> predicate, IRandom rand = null)
		{
			Contracts.CheckValue<Func<int, bool>>(this._host, predicate, "predicate");
			return this._view.GetRowCursor(predicate, rand);
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x0006DE32 File Offset: 0x0006C032
		public IRowCursor[] GetRowCursorSet(out IRowCursorConsolidator consolidator, Func<int, bool> predicate, int n, IRandom rand = null)
		{
			Contracts.CheckValue<Func<int, bool>>(this._host, predicate, "predicate");
			return this._view.GetRowCursorSet(ref consolidator, predicate, n, rand);
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x0006DE58 File Offset: 0x0006C058
		public ISlotCursor GetSlotCursor(int col)
		{
			Contracts.CheckParam(this._host, 0 <= col && col < this.Schema.ColumnCount, "col");
			if (this._tschema == null || this._tschema.GetSlotType(col) == null)
			{
				throw Contracts.ExceptParam(this._host, "col", "Bad call to GetSlotCursor on untransposable column '{0}'", new object[] { this.Schema.GetColumnName(col) });
			}
			return this._tview.GetSlotCursor(col);
		}

		// Token: 0x04000B26 RID: 2854
		public const string LoaderSignature = "PipeDataLoader";

		// Token: 0x04000B27 RID: 2855
		private const string RegistrationName = "Composite";

		// Token: 0x04000B28 RID: 2856
		private const int VersionAddedTags = 65538;

		// Token: 0x04000B29 RID: 2857
		private const string TransformDirTemplate = "Transform_{0:000}";

		// Token: 0x04000B2A RID: 2858
		private readonly IDataLoader _loader;

		// Token: 0x04000B2B RID: 2859
		private readonly CompositeDataLoader.TransformEx[] _transforms;

		// Token: 0x04000B2C RID: 2860
		private readonly IDataView _view;

		// Token: 0x04000B2D RID: 2861
		private readonly ITransposeDataView _tview;

		// Token: 0x04000B2E RID: 2862
		private readonly ITransposeSchema _tschema;

		// Token: 0x04000B2F RID: 2863
		private readonly IHost _host;

		// Token: 0x02000385 RID: 901
		public sealed class Arguments
		{
			// Token: 0x04000B33 RID: 2867
			[Argument(4, HelpText = "The data loader", ShortName = "loader")]
			public SubComponent<IDataLoader, SignatureDataLoader> loader;

			// Token: 0x04000B34 RID: 2868
			[Argument(4, HelpText = "Transform", ShortName = "xf")]
			public KeyValuePair<string, SubComponent<IDataTransform, SignatureDataTransform>>[] transform;
		}

		// Token: 0x02000386 RID: 902
		internal struct TransformEx
		{
			// Token: 0x06001384 RID: 4996 RVA: 0x0006DEE1 File Offset: 0x0006C0E1
			public TransformEx(string tag, string argsString, IDataTransform transform)
			{
				this.Tag = tag;
				this.ArgsString = argsString;
				this.Transform = transform;
			}

			// Token: 0x04000B35 RID: 2869
			public readonly string Tag;

			// Token: 0x04000B36 RID: 2870
			public readonly string ArgsString;

			// Token: 0x04000B37 RID: 2871
			public readonly IDataTransform Transform;
		}
	}
}
