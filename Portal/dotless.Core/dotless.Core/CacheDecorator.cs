using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using dotless.Core.Cache;
using dotless.Core.Loggers;

namespace dotless.Core
{
	// Token: 0x02000005 RID: 5
	public class CacheDecorator : ILessEngine
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002354 File Offset: 0x00000554
		// (set) Token: 0x0600001A RID: 26 RVA: 0x0000235C File Offset: 0x0000055C
		public ILogger Logger { get; set; }

		// Token: 0x0600001B RID: 27 RVA: 0x00002365 File Offset: 0x00000565
		public CacheDecorator(ILessEngine underlying, ICache cache)
			: this(underlying, cache, NullLogger.Instance)
		{
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002374 File Offset: 0x00000574
		public CacheDecorator(ILessEngine underlying, ICache cache, ILogger logger)
		{
			this.Underlying = underlying;
			this.Cache = cache;
			this.Logger = logger;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002394 File Offset: 0x00000594
		public string TransformToCss(string source, string fileName)
		{
			string text = this.ComputeContentHash(source);
			string text2 = fileName + text;
			if (!this.Cache.Exists(text2))
			{
				this.Logger.Debug(string.Format("Inserting cache entry for {0}", text2));
				string text3 = this.Underlying.TransformToCss(source, fileName);
				IEnumerable<string> enumerable = new string[] { fileName }.Concat(this.GetImports());
				this.Cache.Insert(text2, enumerable, text3);
				return text3;
			}
			this.Logger.Debug(string.Format("Retrieving cache entry {0}", text2));
			return this.Cache.Retrieve(text2);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000242B File Offset: 0x0000062B
		private string ComputeContentHash(string source)
		{
			return Convert.ToBase64String(SHA1.Create().ComputeHash(Encoding.Default.GetBytes(source)));
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002447 File Offset: 0x00000647
		public IEnumerable<string> GetImports()
		{
			return this.Underlying.GetImports();
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002454 File Offset: 0x00000654
		public void ResetImports()
		{
			this.Underlying.ResetImports();
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002461 File Offset: 0x00000661
		public bool LastTransformationSuccessful
		{
			get
			{
				return this.Underlying.LastTransformationSuccessful;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000022 RID: 34 RVA: 0x0000246E File Offset: 0x0000066E
		// (set) Token: 0x06000023 RID: 35 RVA: 0x0000247B File Offset: 0x0000067B
		public string CurrentDirectory
		{
			get
			{
				return this.Underlying.CurrentDirectory;
			}
			set
			{
				this.Underlying.CurrentDirectory = value;
			}
		}

		// Token: 0x04000003 RID: 3
		public readonly ILessEngine Underlying;

		// Token: 0x04000004 RID: 4
		public readonly ICache Cache;
	}
}
