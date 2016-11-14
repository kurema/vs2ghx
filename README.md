# vs2ghx
複数の*.csファイルからGrasshopperファイルを作成する簡単なツールです。
## 使い方
```
vs2ghx a.cs b.cs > out.ghx
```
引数にcsファイルをいくつか指定し、適当なファイルにリダイレクトします。

ghxのベースとなるファイルは存在すれば実行ファイルと同じフォルダに或るbase.ghxを、なければ埋め込まれたリソースを利用します。
そのいずれかのフォルダのうち、``[--Additional Source--]``と書かれた部分を*.csファイルの内容で置き換えます。
## 利用目的
GrasshopperのCSコンポーネントには補完機能がついていますが貧弱です。
VisualStudioの補完機能とエラーチェックを利用してGrasshopper用のライブラリを作成する為に作ったツールです。

Perlで作れば十分だったと思いますが、他人が使う場合はPerlが入っているか怪しいので.netで作りました。
## 注意
*.csファイルのうち、外側のnamespaceの内部のみ結合します。
従って、namespaceを入れ子にしないでください。

正しい例
```
using Rhino;
using Rhino.Geometry;
using Rhino.DocObjects;
using Rhino.Collections;

...

namespace kurema.RhinoTools
{
  public class LSystem
  {
    ...
  }
}
```

上手くいかない例
```
using Rhino;
using Rhino.Geometry;
using Rhino.DocObjects;
using Rhino.Collections;

...

namespace kurema
{
  namespace RhinoTools{
    public class LSystem
    {
      ...
    }
  }
}
```
## アドバイス
VisualStudioのプロジェクトのプロパティを開き、ビルドイベントにてビルド後に毎回実行されるようにすれば自動で同期されて便利です。
なお、この際のカレントディレクトリは実行ファイルのある場所です。(bin/debug等)
ただし、作成したghxファイルを編集後は別のファイル名で保存するようにしましょう。
