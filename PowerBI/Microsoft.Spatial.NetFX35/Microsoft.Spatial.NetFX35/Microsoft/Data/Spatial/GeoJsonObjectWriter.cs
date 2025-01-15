using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Data.Spatial
{
	// Token: 0x0200000C RID: 12
	internal sealed class GeoJsonObjectWriter : GeoJsonWriterBase
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00002B1F File Offset: 0x00000D1F
		internal IDictionary<string, object> JsonObject
		{
			get
			{
				return this.lastCompletedObject as IDictionary<string, object>;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00002B2C File Offset: 0x00000D2C
		private bool IsArray
		{
			get
			{
				return this.containers.Peek() is IList;
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00002B44 File Offset: 0x00000D44
		protected override void StartObjectScope()
		{
			object obj = new Dictionary<string, object>(StringComparer.Ordinal);
			if (this.containers.Count > 0)
			{
				this.AddToScope(obj);
			}
			this.containers.Push(obj);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00002B80 File Offset: 0x00000D80
		protected override void StartArrayScope()
		{
			object obj = new List<object>();
			this.AddToScope(obj);
			this.containers.Push(obj);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00002BA6 File Offset: 0x00000DA6
		protected override void AddPropertyName(string name)
		{
			this.currentPropertyName = name;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00002BAF File Offset: 0x00000DAF
		protected override void AddValue(string value)
		{
			this.AddToScope(value);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00002BB8 File Offset: 0x00000DB8
		protected override void AddValue(double value)
		{
			this.AddToScope(value);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00002BC6 File Offset: 0x00000DC6
		protected override void EndArrayScope()
		{
			this.containers.Pop();
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00002BD4 File Offset: 0x00000DD4
		protected override void EndObjectScope()
		{
			object obj = this.containers.Pop();
			if (this.containers.Count == 0)
			{
				this.lastCompletedObject = obj;
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00002C04 File Offset: 0x00000E04
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

		// Token: 0x06000094 RID: 148 RVA: 0x00002C3C File Offset: 0x00000E3C
		private string GetAndClearCurrentPropertyName()
		{
			string text = this.currentPropertyName;
			this.currentPropertyName = null;
			return text;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00002C58 File Offset: 0x00000E58
		private IList AsList()
		{
			return this.containers.Peek() as IList;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00002C78 File Offset: 0x00000E78
		private IDictionary<string, object> AsDictionary()
		{
			return this.containers.Peek() as IDictionary<string, object>;
		}

		// Token: 0x04000010 RID: 16
		private readonly Stack<object> containers = new Stack<object>();

		// Token: 0x04000011 RID: 17
		private string currentPropertyName;

		// Token: 0x04000012 RID: 18
		private object lastCompletedObject;
	}
}
