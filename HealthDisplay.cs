public class HealthDisplay : MonoBehaviour
{
    private void Start()
    {
        hearts = new Image[container.childCount];
        for (int i = 0; i < container.childCount; ++i)
        {
            hearts[i] = container.GetChild(i).GetComponent<Image>();
        }
    }


    [SerializeField] private Transform container;
    private Image[] hearts;


    public void UpdateHealth(HealtSystem hp)
    {
       
        HideHearts();

        var totalHearts = Mathf.Min(hearts.Length, hp.Health / 10);
        for (int i = 0; i < totalHearts; i++)
        {
            hearts[i].color = Color.red; //, si quieren que sus corazones sean de otro color diferente de rojo,este es la 
                                         // linea que tienen que agregar new Color(1.0f,0.5f,0.0f);
        }
    }

    private void HideHearts() {
        for (int i = 0; i < hearts.Length; i++)
            hearts[i].color = Color.white;
    }
}
