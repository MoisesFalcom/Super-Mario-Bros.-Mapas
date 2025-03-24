// Autor: Moises Falcón Pacheco
// Matrícula: A01801140
// Vista del menú principal: asigna acciones a cada botón de navegación.

using UnityEngine.UIElements;
using System;

public class MainMenuView
{
    // Acciones públicas asignables desde el controlador
    public Action OnStartClicked { set => BtnStart.clicked += value; }
    public Action OnHelpClicked { set => BtnHelp.clicked += value; }
    public Action OnCreditsClicked { set => BtnCredits.clicked += value; }
    public Action OnExitClicked { set => BtnExit.clicked += value; }

    // Referencias a botones
    private Button BtnStart;
    private Button BtnHelp;
    private Button BtnCredits;
    private Button BtnExit;

    // Constructor que vincula los botones
    public MainMenuView(VisualElement RootElement)
    {
        BtnHelp = RootElement.Q<Button>("ayuda");
        BtnStart = RootElement.Q<Button>("start");
        BtnCredits = RootElement.Q<Button>("creditos");
        BtnExit = RootElement.Q<Button>("exit");
    }
}
