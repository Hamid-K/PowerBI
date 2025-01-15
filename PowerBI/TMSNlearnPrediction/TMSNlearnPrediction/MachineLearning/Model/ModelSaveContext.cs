using System;
using System.IO;
using System.Text;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Model
{
	// Token: 0x02000475 RID: 1141
	public sealed class ModelSaveContext : IDisposable
	{
		// Token: 0x1700024C RID: 588
		// (get) Token: 0x060017D3 RID: 6099 RVA: 0x00088F4C File Offset: 0x0008714C
		public bool InRepository
		{
			get
			{
				return this.Repository != null;
			}
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x00088F5C File Offset: 0x0008715C
		public ModelSaveContext(RepositoryWriter rep, string dir, string name)
		{
			Contracts.CheckValue<RepositoryWriter>(rep, "rep");
			Contracts.CheckNonEmpty(name, "name");
			this.Repository = rep;
			this.Directory = dir;
			this.Strings = new NormStr.Pool();
			this._ent = rep.CreateEntry(dir, name);
			try
			{
				this.Writer = new BinaryWriter(this._ent.Stream, Encoding.UTF8, true);
				try
				{
					ModelHeader.BeginWrite(this.Writer, out this.FpMin, out this.Header);
				}
				catch
				{
					this.Writer.Dispose();
					throw;
				}
			}
			catch
			{
				this._ent.Dispose();
				throw;
			}
		}

		// Token: 0x060017D5 RID: 6101 RVA: 0x0008901C File Offset: 0x0008721C
		public ModelSaveContext(BinaryWriter writer)
		{
			Contracts.CheckValue<BinaryWriter>(writer, "writer");
			this.Repository = null;
			this.Directory = null;
			this._ent = null;
			this.Strings = new NormStr.Pool();
			this.Writer = writer;
			ModelHeader.BeginWrite(this.Writer, out this.FpMin, out this.Header);
		}

		// Token: 0x060017D6 RID: 6102 RVA: 0x00089078 File Offset: 0x00087278
		public void CheckAtModel()
		{
			Contracts.Check(this.Writer.BaseStream.Position == this.FpMin + this.Header.FpModel);
		}

		// Token: 0x060017D7 RID: 6103 RVA: 0x000890A3 File Offset: 0x000872A3
		public void SetVersionInfo(VersionInfo ver)
		{
			ModelHeader.SetVersionInfo(ref this.Header, ver);
		}

		// Token: 0x060017D8 RID: 6104 RVA: 0x000890B4 File Offset: 0x000872B4
		public void SaveTextStream(string name, Action<TextWriter> action)
		{
			Contracts.Check(this.InRepository, "Can't save a text stream when writing to a single stream");
			Contracts.CheckNonEmpty(name, "name");
			Contracts.CheckValue<Action<TextWriter>>(action, "action");
			using (Repository.Entry entry = this.Repository.CreateEntry(this.Directory, name))
			{
				using (StreamWriter streamWriter = Utils.OpenWriter(entry.Stream, null, 1024, true))
				{
					action(streamWriter);
				}
			}
		}

		// Token: 0x060017D9 RID: 6105 RVA: 0x0008914C File Offset: 0x0008734C
		public void SaveBinaryStream(string name, Action<BinaryWriter> action)
		{
			Contracts.Check(this.InRepository, "Can't save a text stream when writing to a single stream");
			Contracts.CheckNonEmpty(name, "name");
			Contracts.CheckValue<Action<BinaryWriter>>(action, "action");
			using (Repository.Entry entry = this.Repository.CreateEntry(this.Directory, name))
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(entry.Stream, Encoding.UTF8, true))
				{
					action(binaryWriter);
				}
			}
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x000891E0 File Offset: 0x000873E0
		public void SaveStringOrNull(string str)
		{
			if (str == null)
			{
				this.Writer.Write(-1);
				return;
			}
			this.Writer.Write(this.Strings.Add(str).Id);
		}

		// Token: 0x060017DB RID: 6107 RVA: 0x0008920E File Offset: 0x0008740E
		public void SaveString(string str)
		{
			Contracts.CheckValue<string>(str, "str");
			this.Writer.Write(this.Strings.Add(str).Id);
		}

		// Token: 0x060017DC RID: 6108 RVA: 0x00089237 File Offset: 0x00087437
		public void SaveNonEmptyString(string str)
		{
			Contracts.CheckParam(!string.IsNullOrEmpty(str), "str");
			this.Writer.Write(this.Strings.Add(str).Id);
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x00089268 File Offset: 0x00087468
		public void Done()
		{
			Contracts.Check(this.Header.ModelSignature != 0UL, "ModelSignature not specified!");
			ModelHeader.EndWrite(this.Writer, this.FpMin, ref this.Header, this.Strings);
			this.Dispose();
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x000892B4 File Offset: 0x000874B4
		public void Dispose()
		{
			if (this.InRepository)
			{
				this.Writer.Dispose();
				this._ent.Dispose();
			}
		}

		// Token: 0x060017DF RID: 6111 RVA: 0x000892D4 File Offset: 0x000874D4
		public void SaveModel<T>(T value, string name) where T : class
		{
			Contracts.Check(this.InRepository, "Can't save a sub-model when writing to a single stream");
			ModelSaveContext.SaveModel<T>(this.Repository, value, Path.Combine(this.Directory ?? "", name));
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x00089308 File Offset: 0x00087508
		public static void SaveModel<T>(RepositoryWriter rep, T value, string path) where T : class
		{
			if (value == null)
			{
				return;
			}
			ICanSaveModel canSaveModel = value as ICanSaveModel;
			if (canSaveModel != null)
			{
				using (ModelSaveContext modelSaveContext = new ModelSaveContext(rep, path, "Model.key"))
				{
					canSaveModel.Save(modelSaveContext);
					modelSaveContext.Done();
				}
				return;
			}
			ICanSaveInBinaryFormat canSaveInBinaryFormat = value as ICanSaveInBinaryFormat;
			if (canSaveInBinaryFormat != null)
			{
				using (Repository.Entry entry = rep.CreateEntry(path, "Model.bin"))
				{
					using (BinaryWriter binaryWriter = new BinaryWriter(entry.Stream, Encoding.UTF8, true))
					{
						canSaveInBinaryFormat.SaveAsBinary(binaryWriter);
					}
				}
			}
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x000893D0 File Offset: 0x000875D0
		public static void Save(BinaryWriter writer, Action<ModelSaveContext> fn)
		{
			Contracts.CheckValue<BinaryWriter>(writer, "writer");
			Contracts.CheckValue<Action<ModelSaveContext>>(fn, "fn");
			using (ModelSaveContext modelSaveContext = new ModelSaveContext(writer))
			{
				fn(modelSaveContext);
				modelSaveContext.Done();
			}
		}

		// Token: 0x060017E2 RID: 6114 RVA: 0x00089424 File Offset: 0x00087624
		public void SaveSubModel(string dir, Action<ModelSaveContext> fn)
		{
			Contracts.Check(this.InRepository, "Can't save a sub-model when writing to a single stream");
			Contracts.CheckNonEmpty(dir, "dir");
			Contracts.CheckValue<Action<ModelSaveContext>>(fn, "fn");
			using (ModelSaveContext modelSaveContext = new ModelSaveContext(this.Repository, Path.Combine(this.Directory ?? "", dir), "Model.key"))
			{
				fn(modelSaveContext);
				modelSaveContext.Done();
			}
		}

		// Token: 0x04000E66 RID: 3686
		public readonly RepositoryWriter Repository;

		// Token: 0x04000E67 RID: 3687
		public readonly string Directory;

		// Token: 0x04000E68 RID: 3688
		public readonly BinaryWriter Writer;

		// Token: 0x04000E69 RID: 3689
		public readonly NormStr.Pool Strings;

		// Token: 0x04000E6A RID: 3690
		public ModelHeader Header;

		// Token: 0x04000E6B RID: 3691
		public readonly long FpMin;

		// Token: 0x04000E6C RID: 3692
		private readonly Repository.Entry _ent;
	}
}
