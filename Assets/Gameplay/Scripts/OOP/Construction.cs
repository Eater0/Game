using UnityEngine;

class Construction : MonoBehaviour
{
    int side;
    float angle;
    float heigh;
    Vector3 raycastHitOtherY;
    Vector3 direction;
    GameObject actual;

    void Start()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
        GetComponent<BoxCollider>().enabled = false;
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            if (Input.GetKeyDown(KeyCode.R) && !GetComponent<RoofT>() && !GetComponent<StairsT>())
            {
                transform.rotation *= Quaternion.Euler(0, 22.5f, 0);
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                transform.rotation *= Quaternion.Euler(-45, 0, 0);
                transform.rotation *= Quaternion.Euler(0, 22.5f, 0);
                transform.rotation *= Quaternion.Euler(45, 0, 0);
            }

            if (raycastHit.transform.GetComponent<PariesT>() && GetComponent<RoofT>())
            {
                if (actual != raycastHit.transform.gameObject)
                {
                    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, raycastHit.transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
                }

                actual = raycastHit.transform.gameObject;

                transform.position = new Vector3(raycastHit.transform.position.x, raycastHit.transform.position.y + 5.2f, raycastHit.transform.position.z);
            }
            else if (raycastHit.transform.GetComponent<FloorT>() && GetComponent<RoofT>())
            {
                if (actual != raycastHit.transform.gameObject)
                {
                    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, raycastHit.transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
                }

                actual = raycastHit.transform.gameObject;

                transform.position = new Vector3(raycastHit.transform.position.x, raycastHit.transform.position.y + 0.2f, raycastHit.transform.position.z);
            }
            else if (raycastHit.transform.GetComponent<FloorT>() && GetComponent<PariesT>())
            {
                if (actual != raycastHit.transform.gameObject)
                {
                    transform.rotation = raycastHit.transform.rotation;
                }

                actual = raycastHit.transform.gameObject;

                raycastHitOtherY = raycastHit.point;
                raycastHitOtherY.y = raycastHit.transform.position.y;
                direction = raycastHitOtherY - raycastHit.transform.position;

                if (direction.x <= 0) side = 1;
                else side = -1;

                angle = Vector3.Angle(direction, Vector3.forward) * side;
                angle += raycastHit.transform.rotation.eulerAngles.y;

                if (angle > 180) angle = -(180 - (angle - 180));

                heigh = raycastHit.transform.position.y + raycastHit.transform.GetComponent<BoxCollider>().bounds.size.y * raycastHit.transform.localScale.y;

                if (angle >= -45 && angle <= 45)        transform.position = new Vector3(raycastHit.transform.position.x, heigh, raycastHit.transform.position.z + 2);
                else if (angle < -45 && angle >= -135)  transform.position = new Vector3(raycastHit.transform.position.x + 2, heigh, raycastHit.transform.position.z);
                else if (angle > 45 && angle <= 135)    transform.position = new Vector3(raycastHit.transform.position.x - 2, heigh, raycastHit.transform.position.z);
                else                                    transform.position = new Vector3(raycastHit.transform.position.x, heigh, raycastHit.transform.position.z - 2);

                transform.position = raycastHit.transform.rotation * (transform.position - raycastHit.transform.position) + raycastHit.transform.position;
            }
            else if (raycastHit.transform.GetComponent<PariesT>() && GetComponent<PariesT>())
            {
                if (actual != raycastHit.transform.gameObject)
                {
                    transform.rotation = raycastHit.transform.rotation;
                }

                raycastHitOtherY = raycastHit.transform.position;
                raycastHitOtherY.y += raycastHit.transform.GetComponent<BoxCollider>().bounds.size.y * raycastHit.transform.localScale.y / 2;
                direction = raycastHit.point - raycastHitOtherY;

                if (Vector3.Dot(direction.normalized, (raycastHit.transform.right * raycastHit.transform.GetComponent<BoxCollider>().bounds.size.y * raycastHit.transform.localScale.y / 2).normalized) <= 0)   side = 1;
                else    side = -1;

                angle = Vector3.Angle(direction, Vector3.up) * side;

                if (angle >= -45 && angle <= 45)        transform.position = new Vector3(raycastHit.transform.position.x, raycastHit.transform.position.y + 5, raycastHit.transform.position.z);
                else if (angle < -45 && angle >= -135)  transform.position = new Vector3(raycastHit.transform.position.x + 5, raycastHit.transform.position.y, raycastHit.transform.position.z);
                else if (angle > 45 && angle <= 135)    transform.position = new Vector3(raycastHit.transform.position.x - 5, raycastHit.transform.position.y, raycastHit.transform.position.z);
                else                                    transform.position = new Vector3(raycastHit.transform.position.x, raycastHit.transform.position.y - 5, raycastHit.transform.position.z);

                transform.position = raycastHit.transform.rotation * (transform.position - raycastHit.transform.position) + raycastHit.transform.position;
            }
            else if (raycastHit.transform.GetComponent<WallT>() && GetComponent<WallT>())
            {
                if (actual != raycastHit.transform.gameObject)
                {
                    transform.rotation = raycastHit.transform.rotation;
                }

                raycastHitOtherY = raycastHit.transform.position;
                raycastHitOtherY.y += raycastHit.transform.GetComponent<BoxCollider>().bounds.size.y * raycastHit.transform.localScale.y / 2;
                direction = raycastHit.point - raycastHitOtherY;

                if (Vector3.Dot(direction.normalized, (raycastHit.transform.right * raycastHit.transform.GetComponent<BoxCollider>().bounds.size.y * raycastHit.transform.localScale.y / 2).normalized) <= 0) side = 1;
                else side = -1;

                angle = Vector3.Angle(direction, Vector3.up) * side;

                if (angle <= 0 && angle >= -180)    transform.position = new Vector3(raycastHit.transform.position.x + 5, raycastHit.transform.position.y, raycastHit.transform.position.z);
                else                                transform.position = new Vector3(raycastHit.transform.position.x - 5, raycastHit.transform.position.y, raycastHit.transform.position.z);

                transform.position = raycastHit.transform.rotation * (transform.position - raycastHit.transform.position) + raycastHit.transform.position;
            }
            else if (raycastHit.transform.GetComponent<FloorT>() && GetComponent<FloorT>())
            {
                if (actual != raycastHit.transform.gameObject)
                {
                    transform.rotation = raycastHit.transform.rotation;
                }

                actual = raycastHit.transform.gameObject;

                raycastHitOtherY = raycastHit.point;
                raycastHitOtherY.y = raycastHit.transform.position.y;
                direction = raycastHitOtherY - raycastHit.transform.position;

                if (direction.x <= 0)  side = 1;
                else                   side = -1;

                angle = Vector3.Angle(direction, Vector3.forward) * side;
                angle += raycastHit.transform.rotation.eulerAngles.y;
                
                if (angle > 180)    angle = -(180 - (angle - 180));

                if (angle >= -45 && angle <= 45)        transform.position = new Vector3(raycastHit.transform.position.x, raycastHit.transform.position.y, raycastHit.transform.position.z + 5);
                else if (angle < -45 && angle >= -135)  transform.position = new Vector3(raycastHit.transform.position.x + 5, raycastHit.transform.position.y, raycastHit.transform.position.z);
                else if (angle > 45 && angle <= 135)    transform.position = new Vector3(raycastHit.transform.position.x - 5, raycastHit.transform.position.y, raycastHit.transform.position.z);
                else                                    transform.position = new Vector3(raycastHit.transform.position.x, raycastHit.transform.position.y, raycastHit.transform.position.z - 5);

                transform.position = raycastHit.transform.rotation * (transform.position - raycastHit.transform.position) + raycastHit.transform.position;
            }
            else if (raycastHit.transform.GetComponent<PariesT>() && GetComponent<FloorT>())
            {
                if (actual != raycastHit.transform.gameObject)
                {
                    transform.rotation = raycastHit.transform.rotation;
                }

                actual = raycastHit.transform.gameObject;

                raycastHitOtherY = raycastHit.point;
                raycastHitOtherY.y = raycastHit.transform.position.y + 5;
                direction = raycastHitOtherY - (raycastHit.transform.position + new Vector3(0, 5, 0));

                if (Vector3.Dot(direction.normalized, (raycastHit.transform.forward * raycastHit.transform.GetComponent<BoxCollider>().bounds.size.y * raycastHit.transform.localScale.y).normalized) <= 0)   side = 1;
                else    side = -1;

                angle = Vector3.Angle(direction, Vector3.up) * side;

                if (angle == 90)        transform.position = new Vector3(raycastHit.transform.position.x, raycastHitOtherY.y, raycastHit.transform.position.z - 2);
                else                    transform.position = new Vector3(raycastHit.transform.position.x, raycastHitOtherY.y, raycastHit.transform.position.z + 2);

                transform.position = raycastHit.transform.rotation * (transform.position - raycastHit.transform.position) + raycastHit.transform.position;
            }
            else if (raycastHit.transform.GetComponent<FloorT>() && GetComponent<StairsT>())
            {
                if (actual != raycastHit.transform.gameObject)
                {
                    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, raycastHit.transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
                }

                actual = raycastHit.transform.gameObject;

                raycastHitOtherY = raycastHit.transform.position;
                raycastHitOtherY.y += 2.5f;

                transform.position = raycastHitOtherY;
            }
            else
            {
                transform.position = new Vector3(Mathf.RoundToInt(raycastHit.point.x), raycastHit.point.y, Mathf.RoundToInt(raycastHit.point.z));
            }

            if (Input.GetMouseButtonDown(0))
            {
                transform.GetComponent<BoxCollider>().enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
                Destroy(GetComponent<Construction>());
            }
        }
    }
}
