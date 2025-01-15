using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Spatial
{
	// Token: 0x02000006 RID: 6
	internal sealed class GeoJsonObjectWriter : GeoJsonWriterBase
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000035 RID: 53 RVA: 0x0000256F File Offset: 0x0000076F
		internal IDictionary<string, object> JsonObject
		{
			get
			{
				return this.lastCompletedObject as IDictionary<string, object>;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000036 RID: 54 RVA: 0x0000257C File Offset: 0x0000077C
		private bool IsArray
		{
			get
			{
				return this.containers.Peek() is IList;
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002594 File Offset: 0x00000794
		protected override void StartObjectScope()
		{
			object obj = new Dictionary<string, object>(StringComparer.Ordinal);
			if (this.containers.Count > 0)
			{
				this.AddToScope(obj);
			}
			this.containers.Push(obj);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000025D0 File Offset: 0x000007D0
		protected override void StartArrayScope()
		{
			object obj = new List<object>();
			this.AddToScope(obj);
			this.containers.Push(obj);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000025F6 File Offset: 0x000007F6
		protected override void AddPropertyName(string name)
		{
			this.currentPropertyName = name;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000025FF File Offset: 0x000007FF
		protected override void AddValue(string value)
		{
			this.AddToScope(value);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002608 File Offset: 0x00000808
		protected override void AddValue(double value)
		{
			this.AddToScope(value);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002616 File Offset: 0x00000816
		protected override void EndArrayScope()
		{
			this.containers.Pop();
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002624 File Offset: 0x00000824
		protected override void EndObjectScope()
		{
			object obj = this.containers.Pop();
			if (this.containers.Count == 0)
			{
				this.lastCompletedObject = obj;
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002654 File Offset: 0x00000854
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

		// Token: 0x0600003F RID: 63 RVA: 0x0000268C File Offset: 0x0000088C
		private string GetAndClearCurrentPropertyName()
		{
			string text = this.currentPropertyName;
			this.currentPropertyName = null;
			return text;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000026A8 File Offset: 0x000008A8
		private IList AsList()
		{
			return this.containers.Peek() as IList;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000026C8 File Offset: 0x000008C8
		private IDictionary<string, object> AsDictionary()
		{
			return this.containers.Peek() as IDictionary<string, object>;
		}

		// Token: 0x0400000D RID: 13
		private readonly Stack<object> containers = new Stack<object>();

		// Token: 0x0400000E RID: 14
		private string currentPropertyName;

		// Token: 0x0400000F RID: 15
		private object lastCompletedObject;
	}
}
