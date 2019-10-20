

$kialiPod = kubectl get pod -n istio-system -l app=kiali -o jsonpath='{.items[0].metadata.name}'

Write-Host "Navigate to http://localhost:20001/kiali/console"

# Port forward kiali
kubectl port-forward -n istio-system $kialiPod 20001:20001