// Autor: Moises Falcón Pacheco
// Matrícula: A01801140
// Vista para la sección de ayuda del menú.

using UnityEngine.UIElements;
using System;

public class HelpSection
{
    // Acción asignable al botón de regreso
    public Action OnBackClicked
    {
        set
        {
            if (BtnBack != null)
                BtnBack.clicked += value;
        }
    }

    private Button BtnBack;

    // Constructor que vincula el botón
    public HelpSection(VisualElement RootElement)
    {
        BtnBack = RootElement.Q<Button>("salida");
    }
}
