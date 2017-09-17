using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MyComponent))]
[CanEditMultipleObjects]
public class MyComponentEditor : Editor
{

    MyComponent _target;
    MyComponent.Type lastType;

    void OnEnable()
    {
        _target = (MyComponent)target; // сохраняем целевой объект как MyComponent
        lastType = _target.type; // сохраняем текущий тип
        _target.Init();
    }

    // Аналогично Update
    public override void OnInspectorGUI()
    {
        // Рисумем UI заданный по умолчанию
        DrawDefaultInspector();

        // Обновляем, при изменении типа
        if (lastType != _target.type)
        {
            // Обновляем вид
            _target.UpdateView();
            // Сбрасываем параметры на те, что сохранены в массиве
			var parametrs = _target.data.Find(x => x.type == _target.type).parametrs;
            _target.parametrs = new MyComponent.Parametrs(parametrs);
            // переназначаем последний тип
            lastType = _target.type;
        }

        // Изменяем массив при нажатии на кнопку
        if (GUILayout.Button("Применить"))
            ApplyChanges();

        GUI.backgroundColor = new Color(0.5f, 0.5f, 0.5f, 1);
        if (GUILayout.Button("Сбросить"))
            ResetChanges();

        // Обновление вида, без затрагивания массива
        _target.UpdateView(_target.parametrs.color, _target.parametrs.count);

    }

    private void ApplyChanges()
    {
        var data = _target.data.Find(x => x.type == _target.type);
		data.parametrs = _target.parametrs;
        // data.parametrs = new MyComponent.Parametrs(data.parametrs); // здесь происходит клонирование класса, чтобы мы имели возможность менять параметры вне зависимсти от массива данных
        data.sprite = _target.GetComponent<Image>().sprite;
        _target.UpdateView();
    }

    private void ResetChanges()
    {
        var data = _target.data.Find(x => x.type == _target.type);
        _target.parametrs = new MyComponent.Parametrs(data.parametrs);
        _target.UpdateView();
    }

}
