import Modal from "../components/Modal";

export default function Alert({
    isOpen,
    title = "Alert",
    message,
    onClose
}) {
    return (
        <Modal isOpen={isOpen} onClose={onClose}>
            <div className="alert-box">
                <h3>{title}</h3>

                <p>{message}</p>

                <div className="alert-actions">
                    <button
                        className="btn-primary"
                        onClick={onClose}
                    >
                        OK
                    </button>
                </div>
            </div>
        </Modal>
    );
}