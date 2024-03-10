using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MapWindow : EditorWindow
{
	// マップウィンドウのサイズ
	const float WINDOW_W = 750.0f;
	const float WINDOW_H = 750.0f;
	// マップのグリッド数
	private int mapSize = 0;
	// グリッドサイズ。親から値をもらう
	private float gridSize = 0.0f;
	// マップデータ
	private string[,] map;
	// グリッドの四角
	private Rect[,] gridRect;
	// 親ウィンドウの参照を持つ
	private MapMainWindow parent;

	// サブウィンドウを開く
	public static MapWindow WillAppear(MapMainWindow _parent)
	{
		MapWindow window = (MapWindow)EditorWindow.GetWindow(typeof(MapWindow), false);
		window.Show();
		window.minSize = new Vector2(WINDOW_W, WINDOW_H);
		window.SetParent(_parent);
		window.init();
		return window;
	}

	private void SetParent(MapMainWindow _parent)
	{
		parent = _parent;
	}

	// サブウィンドウの初期化
	public void init()
	{
		mapSize = parent.MapSize;
		gridSize = parent.GridSize;

		// マップデータを初期化
		map = new string[mapSize, mapSize];
		for (int i = 0; i < mapSize; i++)
		{
			for (int j = 0; j < mapSize; j++)
			{
				map[i, j] = "";
			}
		}
		// グリッドデータを生成
		gridRect = CreateGrid(mapSize);
	}



	void OnGUI()
	{
		// グリッド線を描画する
		for (int yy = 0; yy < mapSize; yy++)
		{
			for (int xx = 0; xx < mapSize; xx++)
			{
				DrawGridLine(gridRect[yy, xx]);
			}
		}

		// クリックされた位置を探して、その場所に画像データを入れる
		Event e = Event.current;
		if (e.type == EventType.MouseDown)
		{
			Vector2 pos = Event.current.mousePosition;
			int xx;
			// x位置を先に計算して、計算回数を減らす
			for (xx = 0; xx < mapSize; xx++)
			{
				Rect r = gridRect[0, xx];
				if (r.x <= pos.x && pos.x <= r.x + r.width)
				{
					break;
				}
			}

			// 後はy位置だけ探す
			for (int yy = 0; yy < mapSize; yy++)
			{
				if (gridRect[yy, xx].Contains(pos))
				{
					// 消しゴムの時はデータを消す
					if (parent.SelectedImagePath.IndexOf("000") > -1)
					{
						map[yy, xx] = "";
					}
					else
					{
						map[yy, xx] = parent.SelectedImagePath;
					}
					Repaint();
					break;
				}
			}
		}

		// 選択した画像を描画する
		for (int yy = 0; yy < mapSize; yy++)
		{
			for (int xx = 0; xx < mapSize; xx++)
			{
				if (map[yy, xx] != null && map[yy, xx].Length > 0)
				{
					Texture2D tex = (Texture2D)AssetDatabase.LoadAssetAtPath(map[yy, xx], typeof(Texture2D));
					GUI.DrawTexture(gridRect[yy, xx], tex);
				}
			}
		}

		// 出力ボタン
		Rect rect = new Rect(0, WINDOW_H - 50, 300, 50);
		GUILayout.BeginArea(rect);
		if (GUILayout.Button("output file", GUILayout.MinWidth(300), GUILayout.MinHeight(50)))
		{
			OutputFile();
		}
		GUILayout.FlexibleSpace();
		GUILayout.EndArea();

	}

	// グリッドデータを生成
	private Rect[,] CreateGrid(int div)
	{
		int sizeW = div;
		int sizeH = div;

		float x = 0.0f;
		float y = 0.0f;
		float w = gridSize;
		float h = gridSize;

		Rect[,] resultRects = new Rect[sizeH, sizeW];

		for (int yy = 0; yy < sizeH; yy++)
		{
			x = 0.0f;
			for (int xx = 0; xx < sizeW; xx++)
			{
				Rect r = new Rect(new Vector2(x, y), new Vector2(w, h));
				resultRects[yy, xx] = r;
				x += w;
			}
			y += h;
		}

		return resultRects;
	}

	// グリッド線を描画
	private void DrawGridLine(Rect r)
	{
		// grid
		Handles.color = new Color(1f, 1f, 1f, 0.5f);

		// upper line
		Handles.DrawLine(
			new Vector2(r.position.x, r.position.y),
			new Vector2(r.position.x + r.size.x, r.position.y));

		// bottom line
		Handles.DrawLine(
			new Vector2(r.position.x, r.position.y + r.size.y),
			new Vector2(r.position.x + r.size.x, r.position.y + r.size.y));

		// left line
		Handles.DrawLine(
			new Vector2(r.position.x, r.position.y),
			new Vector2(r.position.x, r.position.y + r.size.y));

		// right line
		Handles.DrawLine(
			new Vector2(r.position.x + r.size.x, r.position.y),
			new Vector2(r.position.x + r.size.x, r.position.y + r.size.y));
	}

	// ファイルで出力
	private void OutputFile()
	{
		string path = parent.OutputFilePath();

		FileInfo fileInfo = new FileInfo(path);
		StreamWriter sw = fileInfo.AppendText();
		sw.WriteLine(GetMapStrFormat());
		sw.Flush();
		sw.Close();

		// 完了ポップアップ
		EditorUtility.DisplayDialog("MapCreater", "output file success\n" + path, "ok");
	}

	// 出力するマップデータ整形
	private string GetMapStrFormat()
	{
		string result = "";
		for (int i = 0; i < mapSize; i++)
		{
			for (int j = 0; j < mapSize; j++)
			{
				result += OutputDataFormat(map[i, j]);
				if (j < mapSize - 1)
				{
					result += ",";
				}
			}
			result += "\n";
		}
		return result;
	}

	private string OutputDataFormat(string data)
	{
		if (data != null && data.Length > 0)
		{
			string[] tmps = data.Split('/');
			string fileName = tmps[tmps.Length - 1];
			return fileName.Split('.')[0];
		}
		else
		{
			return "";
		}
	}
}
