// Autor: Moises Falcón Pacheco
// Matrícula: A01801140
// Controlador del botón para regresar al menú desde el juego.

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class BackToMenuFromGame : MonoBehaviour
{
    // Referencia al botón de regreso dentro del documento UI
    private Button BtnReturn;

    private void Start()
    {
        // Se obtiene el elemento raíz de la interfaz visual
        var Root = GetComponent<UIDocument>().rootVisualElement;

        // Se busca el botón con el nombre "regresar"
        BtnReturn = Root.Q<Button>("regresar");

        // Al hacer clic, se carga la escena del menú principal
        BtnReturn?.RegisterCallback<ClickEvent>(evt => SceneManager.LoadScene("MainMenu"));
    }
}


