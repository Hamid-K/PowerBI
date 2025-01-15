using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.AnalysisServices.Tabular.Serialization;

namespace Microsoft.AnalysisServices.Tabular.Tmdl
{
	// Token: 0x02000143 RID: 323
	internal sealed class TmdlProject
	{
		// Token: 0x06001522 RID: 5410 RVA: 0x0008E668 File Offset: 0x0008C868
		public TmdlProject(TmdlContentSource contentSource, params TmdlDocument[] documents)
			: this(contentSource)
		{
			if (documents != null && documents.Length != 0)
			{
				for (int i = 0; i < documents.Length; i++)
				{
					documents[i].Project = this;
					this.documents.Add(documents[i]);
				}
			}
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x0008E6A8 File Offset: 0x0008C8A8
		public TmdlProject(TmdlContentSource contentSource, IEnumerable<TmdlDocument> documents)
			: this(contentSource)
		{
			foreach (TmdlDocument tmdlDocument in documents)
			{
				tmdlDocument.Project = this;
				this.Documents.Add(tmdlDocument);
			}
		}

		// Token: 0x06001524 RID: 5412 RVA: 0x0008E704 File Offset: 0x0008C904
		private TmdlProject(TmdlContentSource contentSource)
		{
			this.documents = new List<TmdlDocument>();
			this.ContentSource = contentSource;
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06001525 RID: 5413 RVA: 0x0008E71E File Offset: 0x0008C91E
		public TmdlContentSource ContentSource { get; }

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06001526 RID: 5414 RVA: 0x0008E726 File Offset: 0x0008C926
		public ICollection<TmdlDocument> Documents
		{
			get
			{
				return this.documents;
			}
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x0008E730 File Offset: 0x0008C930
		internal static TmdlProject Create(TmdlObject @object, ISerializationStrategy strategy, bool includeOrderHints)
		{
			TmdlObject tmdlObject;
			if (@object.ObjectType == ObjectType.Database)
			{
				tmdlObject = @object;
				if (tmdlObject.HasAnyChild(true))
				{
					@object = tmdlObject.Children.Single<TmdlObject>();
					tmdlObject.Children.Clear();
				}
				else
				{
					@object = null;
				}
			}
			else
			{
				tmdlObject = null;
			}
			Dictionary<string, List<TmdlObject>> dictionary = new Dictionary<string, List<TmdlObject>>(StringComparer.InvariantCultureIgnoreCase);
			if (@object != null && @object.HasAnyChild(true))
			{
				List<TmdlObject> list = new List<TmdlObject>();
				List<TmdlObject> list2 = new List<TmdlObject>(@object.Children.Where((TmdlObject c) => c.ObjectType == ObjectType.Null));
				foreach (TmdlObject tmdlObject2 in @object.Children.Where((TmdlObject c) => c.ObjectType > ObjectType.Null))
				{
					bool flag;
					string objectLogicalPath = strategy.GetObjectLogicalPath(tmdlObject2.ObjectType, tmdlObject2.Name.Name, out flag);
					List<TmdlObject> list3;
					if (!dictionary.TryGetValue(objectLogicalPath, out list3))
					{
						list3 = new List<TmdlObject>();
						dictionary.Add(objectLogicalPath, list3);
					}
					list3.Add(tmdlObject2);
					if (includeOrderHints && !flag)
					{
						TmdlObject tmdlObject3 = new TmdlObject(tmdlObject2.ObjectType)
						{
							IsReference = true
						};
						if (!tmdlObject2.Name.IsEmpty)
						{
							tmdlObject3.Name = tmdlObject2.Name;
						}
						list.Add(tmdlObject3);
					}
				}
				@object.Children.Clear();
				for (int i = 0; i < list2.Count; i++)
				{
					@object.Children.Add(list2[i]);
				}
				bool flag2;
				string objectLogicalPath2 = strategy.GetObjectLogicalPath(@object.ObjectType, @object.Name.Name, out flag2);
				List<TmdlObject> list4;
				if (!dictionary.TryGetValue(objectLogicalPath2, out list4))
				{
					list4 = new List<TmdlObject>();
					dictionary.Add(objectLogicalPath2, list4);
				}
				list4.Insert(0, @object);
				list4.AddRange(list);
			}
			if (tmdlObject != null)
			{
				bool flag2;
				string objectLogicalPath3 = strategy.GetObjectLogicalPath(tmdlObject.ObjectType, tmdlObject.Name.Name, out flag2);
				dictionary[objectLogicalPath3] = new List<TmdlObject> { tmdlObject };
			}
			TmdlProject tmdlProject = new TmdlProject(TmdlContentSource.InMemory);
			foreach (KeyValuePair<string, List<TmdlObject>> keyValuePair in dictionary)
			{
				TmdlDocument tmdlDocument = new TmdlDocument(tmdlProject, keyValuePair.Key);
				for (int j = 0; j < keyValuePair.Value.Count; j++)
				{
					tmdlDocument.Objects.Add(keyValuePair.Value[j]);
				}
				tmdlProject.Documents.Add(tmdlDocument);
			}
			return tmdlProject;
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x0008EA08 File Offset: 0x0008CC08
		internal void Serialize(IMetadataSerializationController controller, object userContext)
		{
			Dictionary<string, TmdlDocument> dictionary = new Dictionary<string, TmdlDocument>(StringComparer.InvariantCultureIgnoreCase);
			foreach (TmdlDocument tmdlDocument in this.documents)
			{
				string text = (string.IsNullOrEmpty(tmdlDocument.Path) ? "./unknown" : tmdlDocument.Path);
				if (dictionary.ContainsKey(text))
				{
					int num = 0;
					string text2 = text;
					do
					{
						text = string.Format(CultureInfo.InvariantCulture, "{0}{1}", text2, num);
					}
					while (dictionary.ContainsKey(text));
				}
				dictionary.Add(text, tmdlDocument);
			}
			object obj;
			controller.OnSerializationStart(userContext, new List<string>(dictionary.Keys), out obj);
			bool flag = false;
			try
			{
				foreach (KeyValuePair<string, TmdlDocument> keyValuePair in dictionary)
				{
					object obj2;
					Stream stream;
					controller.OnDocumentSerializationStart(userContext, obj, keyValuePair.Key, out obj2, out stream);
					bool flag2 = false;
					try
					{
						using (TmdlTextWriter tmdlTextWriter = new TmdlTextWriter(stream, default(TmdlWriterSettings)))
						{
							List<TmdlObject> list = new List<TmdlObject>();
							foreach (TmdlObject tmdlObject in keyValuePair.Value.Objects)
							{
								if (tmdlObject.IsEmptyReferenceStub())
								{
									list.Add(tmdlObject);
								}
								else
								{
									tmdlObject.WriteTo(tmdlTextWriter);
								}
							}
							if (list.Count > 0)
							{
								TmdlObject.WriteIndexingRefObjects(tmdlTextWriter, list);
							}
						}
						flag2 = true;
					}
					finally
					{
						controller.OnDocumentSerializationEnd(userContext, obj, keyValuePair.Key, obj2, stream, flag2);
					}
				}
				flag = true;
			}
			catch (Exception ex)
			{
				flag = false;
				controller.OnSerializationError(userContext, obj, ex);
				throw;
			}
			finally
			{
				controller.OnSerializationEnd(userContext, obj, flag);
			}
		}

		// Token: 0x06001529 RID: 5417 RVA: 0x0008EC80 File Offset: 0x0008CE80
		internal void Analyze(out TmdlObject db, out TmdlObject model)
		{
			db = null;
			model = null;
			List<TmdlObject> list = new List<TmdlObject>();
			foreach (TmdlDocument tmdlDocument in this.documents)
			{
				bool flag = false;
				foreach (TmdlObject tmdlObject in tmdlDocument.Objects)
				{
					ObjectType objectType = tmdlObject.ObjectType;
					if (objectType != ObjectType.Model)
					{
						if (objectType == ObjectType.Database)
						{
							if (db == null)
							{
								db = tmdlObject;
							}
							else
							{
								db.AddContentOf(tmdlObject);
							}
						}
					}
					else
					{
						flag = true;
						if (model == null)
						{
							model = tmdlObject;
						}
						else
						{
							model.AddContentOf(tmdlObject);
						}
					}
				}
				foreach (TmdlObject tmdlObject2 in tmdlDocument.Objects.Where((TmdlObject o) => o.ObjectType != ObjectType.Database && o.ObjectType != ObjectType.Model))
				{
					if (flag)
					{
						model.Children.Add(tmdlObject2);
					}
					else
					{
						list.Add(tmdlObject2);
					}
				}
			}
			if (model == null)
			{
				model = new TmdlObject(ObjectType.Model);
			}
			for (int i = 0; i < list.Count; i++)
			{
				model.Children.Add(list[i]);
			}
		}

		// Token: 0x04000395 RID: 917
		private List<TmdlDocument> documents;
	}
}
