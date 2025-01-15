using System;
using System.IO;
using System.Text;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Model
{
	// Token: 0x02000474 RID: 1140
	public sealed class ModelLoadContext : IDisposable
	{
		// Token: 0x1700024B RID: 587
		// (get) Token: 0x060017B8 RID: 6072 RVA: 0x00088799 File Offset: 0x00086999
		public bool InRepository
		{
			get
			{
				return this.Repository != null;
			}
		}

		// Token: 0x060017B9 RID: 6073 RVA: 0x000887A8 File Offset: 0x000869A8
		public ModelLoadContext(RepositoryReader rep, Repository.Entry ent, string dir)
		{
			Contracts.CheckValue<RepositoryReader>(rep, "rep");
			Contracts.CheckValue<Repository.Entry>(ent, "ent");
			this.Repository = rep;
			this.Directory = dir;
			this.Reader = new BinaryReader(ent.Stream, Encoding.UTF8, true);
			try
			{
				ModelHeader.BeginRead(out this.FpMin, out this.Header, out this.Strings, this.Reader);
			}
			catch
			{
				this.Reader.Dispose();
				throw;
			}
		}

		// Token: 0x060017BA RID: 6074 RVA: 0x00088834 File Offset: 0x00086A34
		public ModelLoadContext(BinaryReader reader)
		{
			Contracts.CheckValue<BinaryReader>(reader, "reader");
			this.Repository = null;
			this.Directory = null;
			this.Reader = reader;
			ModelHeader.BeginRead(out this.FpMin, out this.Header, out this.Strings, this.Reader);
		}

		// Token: 0x060017BB RID: 6075 RVA: 0x00088884 File Offset: 0x00086A84
		public void CheckAtModel()
		{
			Contracts.Check(this.Reader.BaseStream.Position == this.FpMin + this.Header.FpModel);
		}

		// Token: 0x060017BC RID: 6076 RVA: 0x000888AF File Offset: 0x00086AAF
		public void CheckAtModel(VersionInfo ver)
		{
			Contracts.Check(this.Reader.BaseStream.Position == this.FpMin + this.Header.FpModel);
			ModelHeader.CheckVersionInfo(ref this.Header, ver);
		}

		// Token: 0x060017BD RID: 6077 RVA: 0x000888E6 File Offset: 0x00086AE6
		public void CheckVersionInfo(VersionInfo ver)
		{
			ModelHeader.CheckVersionInfo(ref this.Header, ver);
		}

		// Token: 0x060017BE RID: 6078 RVA: 0x000888F4 File Offset: 0x00086AF4
		public string LoadStringOrNull()
		{
			int num = this.Reader.ReadInt32();
			Contracts.CheckDecode(-1 <= num && num < Utils.Size<string>(this.Strings));
			if (num >= 0)
			{
				return this.Strings[num];
			}
			return null;
		}

		// Token: 0x060017BF RID: 6079 RVA: 0x00088938 File Offset: 0x00086B38
		public string LoadString()
		{
			int num = this.Reader.ReadInt32();
			Contracts.CheckDecode(0 <= num && num < Utils.Size<string>(this.Strings));
			return this.Strings[num];
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x00088974 File Offset: 0x00086B74
		public string LoadNonEmptyString()
		{
			int num = this.Reader.ReadInt32();
			Contracts.CheckDecode(0 <= num && num < Utils.Size<string>(this.Strings));
			string text = this.Strings[num];
			Contracts.CheckDecode(text.Length > 0);
			return text;
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x000889BF File Offset: 0x00086BBF
		public void Done()
		{
			ModelHeader.EndRead(this.FpMin, ref this.Header, this.Reader);
			this.Dispose();
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x000889DE File Offset: 0x00086BDE
		public void Dispose()
		{
			if (this.InRepository)
			{
				this.Reader.Dispose();
			}
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x000889F4 File Offset: 0x00086BF4
		public bool ContainsModel(string name)
		{
			if (!this.InRepository)
			{
				return false;
			}
			if (string.IsNullOrEmpty(name))
			{
				return false;
			}
			string text = Path.Combine(this.Directory ?? "", name);
			Repository.Entry entry = this.Repository.OpenEntryOrNull(text, "Model.key");
			if (entry != null)
			{
				entry.Dispose();
				return true;
			}
			if ((entry = this.Repository.OpenEntryOrNull(text, "Model.bin")) != null)
			{
				entry.Dispose();
				return true;
			}
			return false;
		}

		// Token: 0x060017C4 RID: 6084 RVA: 0x00088A68 File Offset: 0x00086C68
		public static bool LoadModelOrNull<TRes, TSig>(out TRes result, RepositoryReader rep, string dir, params object[] extra) where TRes : class
		{
			Repository.Entry entry = rep.OpenEntryOrNull(dir, "Model.key");
			if (entry != null)
			{
				using (entry)
				{
					ModelLoadContext.LoadModel<TRes, TSig>(out result, rep, entry, dir, extra);
					return true;
				}
			}
			if ((entry = rep.OpenEntryOrNull(dir, "Model.bin")) != null)
			{
				using (entry)
				{
					ModelLoadContext.LoadModel<TRes, TSig>(out result, entry.Stream, extra);
					return true;
				}
			}
			result = default(TRes);
			return false;
		}

		// Token: 0x060017C5 RID: 6085 RVA: 0x00088AF4 File Offset: 0x00086CF4
		public static void LoadModel<TRes, TSig>(out TRes result, RepositoryReader rep, string dir, params object[] extra) where TRes : class
		{
			if (!ModelLoadContext.LoadModelOrNull<TRes, TSig>(out result, rep, dir, extra))
			{
				throw Contracts.ExceptDecode("Corrupt model file");
			}
		}

		// Token: 0x060017C6 RID: 6086 RVA: 0x00088B0C File Offset: 0x00086D0C
		public bool LoadModelOrNull<TRes, TSig>(out TRes result, string name, params object[] extra) where TRes : class
		{
			Contracts.Check(this.InRepository, "Can't load a sub-model when reading from a single stream");
			return ModelLoadContext.LoadModelOrNull<TRes, TSig>(out result, this.Repository, Path.Combine(this.Directory ?? "", name), extra);
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x00088B40 File Offset: 0x00086D40
		public void LoadModel<TRes, TSig>(out TRes result, string name, params object[] extra) where TRes : class
		{
			if (!this.LoadModelOrNull<TRes, TSig>(out result, name, extra))
			{
				throw Contracts.ExceptDecode("Corrupt model file");
			}
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x00088B58 File Offset: 0x00086D58
		public static bool TryLoadModel<TRes, TSig>(out TRes result, RepositoryReader rep, Repository.Entry ent, string dir, params object[] extra) where TRes : class
		{
			long position = ent.Stream.Position;
			using (ModelLoadContext modelLoadContext = new ModelLoadContext(rep, ent, dir))
			{
				if (modelLoadContext.TryLoadModelCore<TRes, TSig>(out result, extra))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060017C9 RID: 6089 RVA: 0x00088BA8 File Offset: 0x00086DA8
		public static void LoadModel<TRes, TSig>(out TRes result, RepositoryReader rep, Repository.Entry ent, string dir, params object[] extra) where TRes : class
		{
			if (!ModelLoadContext.TryLoadModel<TRes, TSig>(out result, rep, ent, dir, extra))
			{
				throw Contracts.ExceptDecode("Couldn't load model: '{0}'", new object[] { dir });
			}
		}

		// Token: 0x060017CA RID: 6090 RVA: 0x00088BDC File Offset: 0x00086DDC
		public static bool TryLoadModel<TRes, TSig>(out TRes result, Stream stream, params object[] extra) where TRes : class
		{
			bool flag;
			using (BinaryReader binaryReader = new BinaryReader(stream, Encoding.UTF8, true))
			{
				flag = ModelLoadContext.TryLoadModel<TRes, TSig>(out result, binaryReader, extra);
			}
			return flag;
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x00088C1C File Offset: 0x00086E1C
		public static void LoadModel<TRes, TSig>(out TRes result, Stream stream, params object[] extra) where TRes : class
		{
			if (!ModelLoadContext.TryLoadModel<TRes, TSig>(out result, stream, extra))
			{
				throw Contracts.ExceptDecode("Couldn't load model");
			}
		}

		// Token: 0x060017CC RID: 6092 RVA: 0x00088C34 File Offset: 0x00086E34
		public static bool TryLoadModel<TRes, TSig>(out TRes result, BinaryReader reader, params object[] extra) where TRes : class
		{
			long position = reader.BaseStream.Position;
			bool flag;
			using (ModelLoadContext modelLoadContext = new ModelLoadContext(reader))
			{
				flag = modelLoadContext.TryLoadModelCore<TRes, TSig>(out result, extra);
			}
			return flag;
		}

		// Token: 0x060017CD RID: 6093 RVA: 0x00088C7C File Offset: 0x00086E7C
		public static void LoadModel<TRes, TSig>(out TRes result, BinaryReader reader, params object[] extra) where TRes : class
		{
			if (!ModelLoadContext.TryLoadModel<TRes, TSig>(out result, reader, extra))
			{
				throw Contracts.ExceptDecode("Couldn't load model");
			}
		}

		// Token: 0x060017CE RID: 6094 RVA: 0x00088C94 File Offset: 0x00086E94
		private bool TryLoadModelCore<TRes, TSig>(out TRes result, params object[] extra) where TRes : class
		{
			object[] array = ModelLoadContext.ConcatArgsRev(extra, new object[] { this });
			string loaderSig = ModelHeader.GetLoaderSig(ref this.Header);
			object obj;
			if (!string.IsNullOrWhiteSpace(loaderSig) && ComponentCatalog.TryCreateInstance<object, TSig>(ref obj, loaderSig, "", array))
			{
				result = obj as TRes;
				if (result != null)
				{
					this.Done();
					return true;
				}
			}
			string loaderSigAlt = ModelHeader.GetLoaderSigAlt(ref this.Header);
			if (!string.IsNullOrWhiteSpace(loaderSigAlt) && ComponentCatalog.TryCreateInstance<object, TSig>(ref obj, loaderSigAlt, "", array))
			{
				result = obj as TRes;
				if (result != null)
				{
					this.Done();
					return true;
				}
			}
			this.Reader.BaseStream.Position = this.FpMin;
			result = default(TRes);
			return false;
		}

		// Token: 0x060017CF RID: 6095 RVA: 0x00088D69 File Offset: 0x00086F69
		private static object[] ConcatArgsRev(object[] args2, params object[] args1)
		{
			return Utils.Concat<object>(args1, args2);
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x00088D74 File Offset: 0x00086F74
		public bool TryProcessSubModel(string dir, Action<ModelLoadContext> action)
		{
			Contracts.Check(this.InRepository, "Can't Load a sub-model when reading from a single stream");
			Contracts.CheckNonEmpty(dir, "dir");
			Contracts.CheckValue<Action<ModelLoadContext>>(action, "action");
			string text = Path.Combine(this.Directory, dir);
			Repository.Entry entry = this.Repository.OpenEntryOrNull(text, "Model.key");
			if (entry == null)
			{
				return false;
			}
			using (entry)
			{
				using (ModelLoadContext modelLoadContext = new ModelLoadContext(this.Repository, entry, text))
				{
					action(modelLoadContext);
				}
			}
			return true;
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x00088E18 File Offset: 0x00087018
		public bool TryLoadBinaryStream(string name, Action<BinaryReader> action)
		{
			Contracts.Check(this.InRepository, "Can't Load a sub-model when reading from a single stream");
			Contracts.CheckNonEmpty(name, "name");
			Contracts.CheckValue<Action<BinaryReader>>(action, "action");
			Repository.Entry entry = this.Repository.OpenEntryOrNull(this.Directory, name);
			if (entry == null)
			{
				return false;
			}
			using (entry)
			{
				using (BinaryReader binaryReader = new BinaryReader(entry.Stream, Encoding.UTF8, true))
				{
					action(binaryReader);
				}
			}
			return true;
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x00088EB4 File Offset: 0x000870B4
		public bool TryLoadTextStream(string name, Action<TextReader> action)
		{
			Contracts.Check(this.InRepository, "Can't Load a sub-model when reading from a single stream");
			Contracts.CheckNonEmpty(name, "name");
			Contracts.CheckValue<Action<TextReader>>(action, "action");
			Repository.Entry entry = this.Repository.OpenEntryOrNull(this.Directory, name);
			if (entry == null)
			{
				return false;
			}
			using (entry)
			{
				using (StreamReader streamReader = new StreamReader(entry.Stream))
				{
					action(streamReader);
				}
			}
			return true;
		}

		// Token: 0x04000E5E RID: 3678
		public const string ModelStreamName = "Model.key";

		// Token: 0x04000E5F RID: 3679
		internal const string NameBinary = "Model.bin";

		// Token: 0x04000E60 RID: 3680
		public readonly RepositoryReader Repository;

		// Token: 0x04000E61 RID: 3681
		public readonly string Directory;

		// Token: 0x04000E62 RID: 3682
		public readonly BinaryReader Reader;

		// Token: 0x04000E63 RID: 3683
		public readonly string[] Strings;

		// Token: 0x04000E64 RID: 3684
		public ModelHeader Header;

		// Token: 0x04000E65 RID: 3685
		public readonly long FpMin;
	}
}
