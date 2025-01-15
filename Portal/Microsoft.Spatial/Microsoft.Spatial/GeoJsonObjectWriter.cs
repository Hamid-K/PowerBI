using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Spatial
{
	// Token: 0x02000006 RID: 6
	internal sealed class GeoJsonObjectWriter : GeoJsonWriterBase
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002957 File Offset: 0x00000B57
		internal IDictionary<string, object> JsonObject
		{
			get
			{
				return this.lastCompletedObject as IDictionary<string, object>;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002964 File Offset: 0x00000B64
		private bool IsArray
		{
			get
			{
				return this.containers.Peek() is IList;
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000297C File Offset: 0x00000B7C
		protected override void StartObjectScope()
		{
			object obj = new Dictionary<string, object>(StringComparer.Ordinal);
			if (this.containers.Count > 0)
			{
				this.AddToScope(obj);
			}
			this.containers.Push(obj);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000029B8 File Offset: 0x00000BB8
		protected override void StartArrayScope()
		{
			object obj = new List<object>();
			this.AddToScope(obj);
			this.containers.Push(obj);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000029DE File Offset: 0x00000BDE
		protected override void AddPropertyName(string name)
		{
			this.currentPropertyName = name;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000029E7 File Offset: 0x00000BE7
		protected override void AddValue(string value)
		{
			this.AddToScope(value);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000029F0 File Offset: 0x00000BF0
		protected override void AddValue(double value)
		{
			this.AddToScope(value);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000029FE File Offset: 0x00000BFE
		protected override void EndArrayScope()
		{
			this.containers.Pop();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002A0C File Offset: 0x00000C0C
		protected override void EndObjectScope()
		{
			object obj = this.containers.Pop();
			if (this.containers.Count == 0)
			{
				this.lastCompletedObject = obj;
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002A3C File Offset: 0x00000C3C
		private void AddToScope(object jsonObject)
		{
			if (this.IsArray)
			{
				this.AsList().Add(jsonObject);
				return;
			}
			string andClearCurrentPropertyName = this.GetAndClearCurrentPropertyName();
			this.AsDictionary().Add(andClearCurrentPropertyName, jsonObject);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002A74 File Offset: 0x00000C74
		private string GetAndClearCurrentPropertyName()
		{
			string text = this.currentPropertyName;
			this.currentPropertyName = null;
			return text;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002A90 File Offset: 0x00000C90
		private IList AsList()
		{
			return this.containers.Peek() as IList;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002AB0 File Offset: 0x00000CB0
		private IDictionary<string, object> AsDictionary()
		{
			return this.containers.Peek() as IDictionary<string, object>;
		}

		// Token: 0x0400000E RID: 14
		private readonly Stack<object> containers = new Stack<object>();

		// Token: 0x0400000F RID: 15
		private string currentPropertyName;

		// Token: 0x04000010 RID: 16
		private object lastCompletedObject;
	}
}
